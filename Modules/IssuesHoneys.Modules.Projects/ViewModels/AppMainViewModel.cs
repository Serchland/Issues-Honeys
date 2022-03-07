using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppMainViewModel : BindableBase
    {
        public AppMainViewModel()
        {
            Message = "Projects Main under construction";
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}
