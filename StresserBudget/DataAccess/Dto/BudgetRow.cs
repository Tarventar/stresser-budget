using System;

namespace DataAccess.Dto
{
    public partial class BudgetRow
    {
        public DtoStatus Status { get; set; } = DtoStatus.Unchanged;

        public static BudgetRow InitEmpty()
        {
            return new BudgetRow()
            {
                Status = DtoStatus.Created,
                IsNull = false,
                ID = -1,
                IDKonto = -1,
                Bezeichnung = string.Empty
            };
        }
    }
}
