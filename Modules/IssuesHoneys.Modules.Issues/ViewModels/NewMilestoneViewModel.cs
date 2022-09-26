using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Regions;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewMilestoneViewModel : ViewModelBase<Milestone>
    {
        public NewMilestoneViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands) : base(regionManager, applicationCommands)
        {
        }
    }
}
