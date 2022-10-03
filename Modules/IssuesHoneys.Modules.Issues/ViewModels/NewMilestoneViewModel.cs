using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Events;
using Prism.Regions;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewMilestoneViewModel : ViewModelBase<Milestone>
    {
        public NewMilestoneViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands, IEventAggregator eventAggregator) : base(regionManager, applicationCommands, eventAggregator)
        {
        }
    }
}
