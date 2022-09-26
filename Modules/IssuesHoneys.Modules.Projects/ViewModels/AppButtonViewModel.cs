using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Regions;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppButtonViewModel : ViewModelBase<object>
    {
        private IApplicationCommands _applicationCommands;
        public AppButtonViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands) : base(regionManager, applicationCommands)
        {
            _applicationCommands = applicationCommands;
        }

        //#region "Commands"
        //private DelegateCommand<string> _navigateCommand;
        //public DelegateCommand<string> NavigateCommand =>
        //    _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        //void ExecuteNavigateCommand(string parameter)
        //{
        //    if (string.IsNullOrEmpty(parameter))
        //        throw new ArgumentNullException(ArgumentExceptionMessage);

        //    _applicationCommands.NavigateCommand.Execute(parameter);
        //}
        //#endregion
    }
}
