using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : BindableBase
    {       
        public AppButtonViewModel()
        {
            _total = "15";
        }

        private string _total;
        public string Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }
    }
}
