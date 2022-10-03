using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Events;
using Prism.Regions;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : ViewModelBase<object>
    {
        public AppButtonViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands, IEventAggregator eventAggregator) : base(regionManager, applicationCommands, eventAggregator)
        {
        }
 
    }
}
