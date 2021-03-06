using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewMilestoneViewModel : ViewModelBase<Milestone>
    {
        public NewMilestoneViewModel(IApplicationCommands applicationCommands) : base(applicationCommands)
        {
        }
    }
}
