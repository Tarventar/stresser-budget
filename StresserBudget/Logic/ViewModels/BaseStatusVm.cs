using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using SdaWpfLib.ViewModelBase;

namespace Logic.ViewModels
{
    public abstract class BaseStatusVm : BaseValidatableVm
    {
        protected void ChangingRow(string aPropertyName)
        {
            if (this.RowStatus == DtoStatus.Unchanged)
            {
                this.RowStatus = DtoStatus.Updated;
            }

            this.NotifyPropertyChanged(aPropertyName);
        }

        protected override IEnumerable<string> ValidateYourself()
        {
            return new string[] { };
        }

        internal abstract DtoStatus RowStatus { get; set; }
    }
}
