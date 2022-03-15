using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppDriveViewModel : BindableBase
    {
        private IApplicationCommands _applicationCommands;

        public AppDriveViewModel(IApplicationCommands applicationCommands) 
        {
            _applicationCommands = applicationCommands;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException("Param cant be null");

            _applicationCommands.NavigateCommand.Execute(param);
        }
    }
}
