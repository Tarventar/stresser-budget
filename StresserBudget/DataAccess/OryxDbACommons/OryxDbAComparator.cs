using System;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// Base class for comparator implementations.
    /// </summary>
    public abstract class OryxDbAComparator : IWhereEmitter
    {
        private string mColumn = null;
        /// <summary>
        /// Gets or sets the SQL valid name of the column to compare.
        /// </summary>
        public string Column 
        {
            get
            {
                return mColumn;
            }
            set
            {
                mColumn = value;
                mCache = null;
            }
        }

        private bool mAllowCaching = true;
        /// <summary>
        /// Allows or disallows caching of built where conditions.
        /// </summary>
        public bool AllowCaching
        {
            get
            {
                return mAllowCaching;
            }
            set
            {
                mAllowCaching = value;
                if (!mAllowCaching)
                    Cache = null;
            }
        }

        private string mCache = null;
        /// <summary>
        /// Gets or sets the cache value.
        /// </summary>
        protected string Cache
        {
            get
            {
                return mCache;
            }
            set
            {
                if (AllowCaching)
                    mCache = value;
                else
                    mCache = null;
            }
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public abstract string EmitWhereCondition();

        protected string GetSqlValue(object value)
        {
            string retVal = null;
            bool escape = true;
            if (value is string)
            {
                retVal = Convert.ToString(value);
            }
            else if (value is bool)
            {
                retVal = ToSQLValue(Convert.ToBoolean(value));
                escape = false;
            }
            else if (value is DateTime)
            {
                retVal = ToSQLValue(Convert.ToDateTime(value));
            }
            else
            {
                string sValue = value.ToString();
                // Check for numbers
                double d; // all 'normal' numbers
                long l; // to support very large numbers
                decimal dec; // to support very large numbers
                if (double.TryParse(sValue, out d) || long.TryParse(sValue, out l) || decimal.TryParse(sValue, out dec))
                {
                    retVal = sValue;
                    escape = false;
                }
                else
                {
                    retVal = sValue;
                }
            }

            if (escape)
            {
                retVal = retVal.Replace("'", "''");
                retVal = "'" + retVal + "'";
            }
            return retVal;
        }

        private string ToSQLValue(bool value)
        {
            if (value)
            {
                return "1";
            }
            return "0";
        }

        private string ToSQLValue(DateTime dateTime)
        {
            return string.Format("{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}",
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second);
        }
    }

    /// <summary>
    /// An instance of this class can be used to compare the value of a single column against a single value.
    /// </summary>
    public class OryxDbASingleValueComparator : OryxDbAComparator
    {
        private object mValue = null;
        /// <summary>
        /// Gets or sets the value to compare against the column value.
        /// </summary>
        public object Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                Cache = null;
            }
        }

        private WhereOperatorSingleValueEnum mWhereOperator;
        /// <summary>
        /// Gets or sets the where operator.
        /// </summary>
        public WhereOperatorSingleValueEnum WhereOperator
        {
            get
            {
                return mWhereOperator;
            }
            set
            {
                mWhereOperator = value;
                Cache = null;
            }
        }

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public OryxDbASingleValueComparator()
        {
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="column">The SQL valid name of the column to compare.</param>
        /// <param name="whereOperator">The kind of comparison to apply.</param>
        /// <param name="value">The value to compare.</param>
        public OryxDbASingleValueComparator(string column, WhereOperatorSingleValueEnum whereOperator, object value)
        {
            Column = column;
            WhereOperator = whereOperator;
            Value = value;
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public override string EmitWhereCondition()
        {
            if (Cache != null)
                return Cache;
            string retVal = null;
            string singleValue = GetSqlValue(Value);
            switch (WhereOperator)
            {
                case WhereOperatorSingleValueEnum.Equal:
                    retVal = Column + " = " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.Like:
                    retVal = Column + " LIKE " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.NotEqual:
                    retVal = Column + " <> " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.GreaterThan:
                    retVal = Column + " > " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.LessThan:
                    retVal = Column + " < " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.GreaterThanOrEqual:
                    retVal = Column + " >= " + singleValue;
                    break;
                case WhereOperatorSingleValueEnum.LessThanOrEqual:
                    retVal = Column + " <= " + singleValue;
                    break;
                default:
                    throw new OryxDbADynamicWhereException(string.Format("Unknown WhereOperator '{0}'.", WhereOperator.ToString()));
            }
            Cache = retVal;
            return retVal;
        }
    }

    /// <summary>
    /// An instance of this class can be used to check if a single column is null or not.
    /// </summary>
    public class OryxDbANullComparator : OryxDbAComparator
    {
        private WhereOperatorNullEnum mWhereOperator;
        /// <summary>
        /// Gets or sets the where operator.
        /// </summary>
        public WhereOperatorNullEnum WhereOperator
        {
            get
            {
                return mWhereOperator;
            }
            set
            {
                mWhereOperator = value;
                Cache = null;
            }
        }

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public OryxDbANullComparator()
        {
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="column">The SQL valid name of the column to compare.</param>
        /// <param name="whereOperator">The kind of comparison to apply.</param>
        public OryxDbANullComparator(string column, WhereOperatorNullEnum whereOperator)
        {
            Column = column;
            WhereOperator = whereOperator;
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public override string EmitWhereCondition()
        {
            if (Cache != null)
                return Cache;
            string retVal = null;
            switch (WhereOperator)
            {
                case WhereOperatorNullEnum.IsNull:
                    retVal = Column + " IS NULL";
                    break;
                case WhereOperatorNullEnum.IsNotNull:
                    retVal = Column + " IS NOT NULL";
                    break;
                default:
                    throw new OryxDbADynamicWhereException(string.Format("Unknown WhereOperator '{0}'.", WhereOperator.ToString()));
            }
            Cache = retVal;
            return retVal;
        }
    }

    /// <summary>
    /// An instance of this class can be used to check if the value of a single column is between two values.
    /// </summary>
    public class OryxDbAMinMaxComparator : OryxDbAComparator
    {
        private object mMinValue = null;
        /// <summary>
        /// Gets or sets the minimum value to compare against the column value.
        /// </summary>
        public object MinValue
        {
            get
            {
                return mMinValue;
            }
            set
            {
                mMinValue = value;
                Cache = null;
            }
        }

        private object mMaxValue = null;
        /// <summary>
        /// Gets or sets the maximum value to compare against the column value.
        /// </summary>
        public object MaxValue
        {
            get
            {
                return mMaxValue;
            }
            set
            {
                mMaxValue = value;
                Cache = null;
            }
        }

        private WhereOperatorMinMaxValueEnum mWhereOperator;
        /// <summary>
        /// Gets or sets the where operator.
        /// </summary>
        public WhereOperatorMinMaxValueEnum WhereOperator
        {
            get
            {
                return mWhereOperator;
            }
            set
            {
                mWhereOperator = value;
                Cache = null;
            }
        }

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public OryxDbAMinMaxComparator()
        {
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="column">The SQL valid name of the column to compare.</param>
        /// <param name="whereOperator">The kind of comparison to apply.</param>
        /// <param name="minValue">The minimum value to compare against.</param>
        /// <param name="maxValue">The maximum value to compare against.</param>
        public OryxDbAMinMaxComparator(string column, WhereOperatorMinMaxValueEnum whereOperator, object minValue, object maxValue)
        {
            Column = column;
            WhereOperator = whereOperator;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public override string EmitWhereCondition()
        {
            if (Cache != null)
                return Cache;
            string retVal = null;
            string minValue = GetSqlValue(MinValue);
            string maxValue = GetSqlValue(MaxValue);

            switch (WhereOperator)
            {
                case WhereOperatorMinMaxValueEnum.Between:
                    retVal = Column + " BETWEEN " + minValue + " AND " + maxValue;
                    break;
                case WhereOperatorMinMaxValueEnum.NotBetween:
                    retVal = Column + " NOT BETWEEN " + minValue + " AND " + maxValue;
                    break;
                default:
                    throw new OryxDbADynamicWhereException(string.Format("Unknown WhereOperator '{0}'.", WhereOperator.ToString()));
            }
            Cache = retVal;
            return retVal;
        }
    }

    /// <summary>
    /// An instance of this class can be used to check if the value of a single column is in a range of values or not.
    /// </summary>
    public class OryxDbAMultipleValueComparator : OryxDbAComparator
    {
        private Array mValues = null;
        /// <summary>
        /// Gets or sets the values to compare against the column value.
        /// </summary>
        public Array Values
        {
            get
            {
                return mValues;
            }
            set
            {
                mValues = value;
                Cache = null;
            }
        }

        private WhereOperatorMultipleValueEnum mWhereOperator;
        /// <summary>
        /// Gets or sets the where operator.
        /// </summary>
        public WhereOperatorMultipleValueEnum WhereOperator
        {
            get
            {
                return mWhereOperator;
            }
            set
            {
                mWhereOperator = value;
                Cache = null;
            }
        }

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public OryxDbAMultipleValueComparator()
        {
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="column">The SQL valid name of the column to compare.</param>
        /// <param name="whereOperator">The kind of comparison to apply.</param>
        /// <param name="values">The values to compare against.</param>
        public OryxDbAMultipleValueComparator(string column, WhereOperatorMultipleValueEnum whereOperator, Array values)
        {
            Column = column;
            WhereOperator = whereOperator;
            Values = values;
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public override string EmitWhereCondition()
        {
            if (Cache != null)
                return Cache;
            string retVal = null;
            StringBuilder sb = new StringBuilder();
            foreach (var value in Values)
            {
                sb.Append(GetSqlValue(value));
                sb.Append(", ");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);

            switch (WhereOperator)
            {
                case WhereOperatorMultipleValueEnum.In:
                    retVal = Column + " IN (" + sb.ToString() + ")";
                    break;
                case WhereOperatorMultipleValueEnum.NotIn:
                    retVal = Column + " NOT IN (" + sb.ToString() + ")";
                    break;
                default:
                    throw new OryxDbADynamicWhereException(string.Format("Unknown WhereOperator '{0}'.", WhereOperator.ToString()));
            }
            Cache = retVal;
            return retVal;
        }
    }
}
