using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace IssuesHoneys.Core.Types.Base
{
    public class AppRibbonTabButtonBase : BindableBase
    {
        IApplicationCommands _applicationCommands;
        public AppRibbonTabButtonBase(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        private DelegateCommand<string> _selectedCommand;
        public DelegateCommand<string> SelectedCommand =>
            _selectedCommand ?? (_selectedCommand = new DelegateCommand<string>(ExecuteSelectedCommand));

        void ExecuteSelectedCommand(string param)
        {
            _applicationCommands.NavigationNavigateCommand.Execute(param);
        }
    }
}
