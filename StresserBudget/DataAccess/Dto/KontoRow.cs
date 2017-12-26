using System;

namespace DataAccess.Dto
{
    public partial class KontoRow
    {
        public DtoStatus Status { get; set; } = DtoStatus.Unchanged;

        public static KontoRow InitEmpty()
        {
            return new KontoRow()
            {
                Status = DtoStatus.Created,
                IsNull = false,
                ID = -1,
                Bezeichnung = string.Empty                
            };
        }
    }
}
