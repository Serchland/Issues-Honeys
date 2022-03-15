using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppButtonViewModel : ViewModelBase 
    {
        public AppButtonViewModel(IApplicationCommands applicationCommand, IIssueService issueService) : base(applicationCommand, issueService)
        {
        }
    }
}
