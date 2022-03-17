using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class LabelsViewModel : BindableBase
    {
        private IApplicationCommands _applicationCommands;

        public LabelsViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        #region "Commands"
        private DelegateCommand<string> _cancelCommand;
        public DelegateCommand<string> CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand<string>(ExecuteCommandName));

        void ExecuteCommandName(string parameter)
        {

        }
        #endregion
    }
}
