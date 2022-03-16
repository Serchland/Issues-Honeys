using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewIssueViewModel : BindableBase
    {
        IApplicationCommands _applicationCommand;
        public NewIssueViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommand = applicationCommands;
        }

        #region "Commands"
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("parameter cant be null");

            _applicationCommand.NavigateCommand.Execute(parameter);
        }
        #endregion
    }
}
