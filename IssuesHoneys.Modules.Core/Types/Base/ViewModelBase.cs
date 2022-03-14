using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace IssuesHoneys.Core.Types.Base
{
    public class ViewModelBase : BindableBase
    {
        IApplicationCommands _applicationCommands;
        public ViewModelBase(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string param)
        {
            if (string.IsNullOrEmpty(param))
                param = CommandParameters.Defautl;

            _applicationCommands.NavigateCommand.Execute(param);
        }
    }
}
