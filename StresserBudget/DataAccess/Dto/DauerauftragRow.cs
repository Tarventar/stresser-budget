using System;

namespace DataAccess.Dto
{
    public partial class DauerauftragRow
    {
        public DtoStatus Status { get; set; } = DtoStatus.Unchanged;

        public static DauerauftragRow InitEmpty()
        {
            return new DauerauftragRow()
            {
                Status = DtoStatus.Created,
                IsNull = false,
                ID = -1,
                IDBudget = -1,
                Bezeichnung = string.Empty,
                GueltigAb = new DateTime(2017, 1, 1),
                GueltigBis = new DateTime(9999, 12, 30),
                Betrag = 0,
                Lauftag = 28
            };
        }
    }
}
