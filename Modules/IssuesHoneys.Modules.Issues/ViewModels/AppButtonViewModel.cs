using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Regions;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : ViewModelBase<object>
    {
        public AppButtonViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands) : base(regionManager, applicationCommands)
        {
        }
 
    }
}
