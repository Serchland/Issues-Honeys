using IssuesHoneys.Core.Types;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppMainViewModel : BindableBase
    {
        IApplicationCommands _applicationsCommands;
        public AppMainViewModel(IApplicationCommands applicationsCommands)
        {
            Message = "Projects Main under construction";
            _applicationsCommands = applicationsCommands;
        }

        #region "Properties"
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        #region "Commands"
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            _applicationsCommands.NavigateCommand.Execute(parameter);
        }
        #endregion
    }
}
