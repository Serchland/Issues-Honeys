using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewIssueViewModel : ViewModelBase
    {
        public NewIssueViewModel(IApplicationCommands applicationCommands, IIssueService issuesService) : base(applicationCommands, issuesService)
        {

        }
    }
}
