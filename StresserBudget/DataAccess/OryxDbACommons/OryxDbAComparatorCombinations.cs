using System.Collections.Generic;

namespace DataAccess
{
    public class OryxDbAComparatorCombination : IWhereEmitter
    {
        private List<OryxDbAComparatorCombination> mCombinations = new List<OryxDbAComparatorCombination>();
        private List<OryxDbAComparator> mComparators = new List<OryxDbAComparator>();

        /// <summary>
        /// Gets or sets the combinator of this collection.
        /// </summary>
        public WhereOperatorCombinatorEnum Combinator { get; set; }

        /// <summary>
        /// Gets the collection of contained combinations.
        /// </summary>
        public List<OryxDbAComparatorCombination> Combinations
        {
            get
            {
                return mCombinations;
            }
        }

        /// <summary>
        /// Gets the collection of contained comparators.
        /// </summary>
        public List<OryxDbAComparator> Comparators
        {
            get
            {
                return mComparators;
            }
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public OryxDbAComparatorCombination()
        {
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="combinator">The combinator to use in this collection.</param>
        public OryxDbAComparatorCombination(WhereOperatorCombinatorEnum combinator)
        {
            Combinator = combinator;
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="combinator">The combinator to use in this collection.</param>
        /// <param name="comperators">Comperator instances to add to this instance.</param>
        public OryxDbAComparatorCombination(WhereOperatorCombinatorEnum combinator, OryxDbAComparator[] comperators)
        {
            Combinator = combinator;
            mComparators.AddRange(comperators);
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="combinator">The combinator to use in this collection.</param>
        /// <param name="subCollections">Sub collections to add to this instance.</param>
        public OryxDbAComparatorCombination(WhereOperatorCombinatorEnum combinator, OryxDbAComparatorCombination[] subCollections)
        {
            Combinator = combinator;
            mCombinations.AddRange(subCollections);
        }

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="combinator">The combinator to use in this collection.</param>
        /// <param name="comperators">Comperator instances to add to this instance.</param>
        /// <param name="subCollections">Sub collections to add to this instance.</param>
        public OryxDbAComparatorCombination(WhereOperatorCombinatorEnum combinator, OryxDbAComparator[] comperators, OryxDbAComparatorCombination[] subCollections)
        {
            Combinator = combinator;
            mComparators.AddRange(comperators);
            mCombinations.AddRange(subCollections);
        }

        /// <summary>
        /// Emits the where condition but without the 'WHERE' keyword
        /// </summary>
        public string EmitWhereCondition()
        {
            string sqlWhere = string.Empty;
            string separator = null;
            if (Combinator == WhereOperatorCombinatorEnum.And)
                separator = " AND ";
            else
                separator = " OR ";

            foreach (OryxDbAComparator oneComparator in mComparators)
            {
                string whereOutput = oneComparator.EmitWhereCondition();
                if (!string.IsNullOrEmpty(whereOutput))
                {
                    if (!string.IsNullOrEmpty(sqlWhere))
                        sqlWhere += separator;
                    sqlWhere += "(" + whereOutput + ")";
                }
            }
            foreach (OryxDbAComparatorCombination oneCombination in mCombinations)
            {
                string whereOutput = oneCombination.EmitWhereCondition();
                if (!string.IsNullOrEmpty(whereOutput))
                {
                    if (!string.IsNullOrEmpty(sqlWhere))
                        sqlWhere += separator;
                    sqlWhere += "(" + whereOutput + ")";
                }
            }
            return sqlWhere;
        }
    }
}
