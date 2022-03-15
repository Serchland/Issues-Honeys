using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppDriveViewModel : ViewModelBase
    {
        public AppDriveViewModel(IApplicationCommands applicationCommands, IIssueService issueService) : base (applicationCommands, issueService)
        {

        }
    }
}
