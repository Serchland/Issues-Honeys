using IssuesHoneys.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Services.Interfaces
{
    public interface IIssueService
    {
        void CreateLabel();
        List<Label> GetLabels();
        List<Millestone> GetMillestones();
        List<Issue> GetIssues();
    }
}
