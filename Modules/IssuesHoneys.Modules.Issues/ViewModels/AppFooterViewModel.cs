using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppFooterViewModel : BindableBase
    {
        public AppFooterViewModel()
        {
            Message = "Issues Footer under construction";
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}
