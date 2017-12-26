using System;

namespace DataAccess.Dto
{
    public partial class BuchungRow
    {
        public DtoStatus Status { get; set; } = DtoStatus.Unchanged;

        public static BuchungRow InitEmpty()
        {
            return new BuchungRow()
            {
                Status = DtoStatus.Created,
                IsNull = false,
                ID = -1,
                IDBudget = -1,
                IDDauerauftrag = -1,
                Betrag = 0,
                Datum = DateTime.Today,
                Bemerkung = string.Empty,
                Bezeichnung = string.Empty
            };
        }
    }
}
