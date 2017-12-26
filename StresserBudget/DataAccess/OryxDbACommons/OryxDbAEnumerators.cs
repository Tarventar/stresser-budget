using System;

namespace DataAccess
{
    public enum WhereOperatorSingleValueEnum
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Equal,
        NotEqual,
        Like
    }

    public enum WhereOperatorNullEnum
    {
        IsNull,
        IsNotNull
    }

    public enum WhereOperatorMinMaxValueEnum
    {
        Between,
        NotBetween
    }

    public enum WhereOperatorMultipleValueEnum
    {
        In,
        NotIn
    }

    public enum WhereOperatorCombinatorEnum
    {
        And,
        Or
    }
    
    public enum OryxFilterCombinationEnum
    {
        And,
        Or
    }
}
