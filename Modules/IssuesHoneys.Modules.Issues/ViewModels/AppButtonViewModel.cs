using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : BindableBase
    {
        private IApplicationCommands _applicationCommands;
        public AppButtonViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("parameter cant be null");

            _applicationCommands.NavigateCommand.Execute(parameter);
        }
    }
}
