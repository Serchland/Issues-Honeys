using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Services
{
    public class IssueSQLService : BindableBase, IIssueService
    {
        public void CreateLabel()
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssues()
        {
            throw new NotImplementedException();
        }

        public List<Label> GetLabels()
        {
            throw new NotImplementedException();
        }

        public List<Milestone> GetMillestones()
        {
            throw new NotImplementedException();
        }
    }
}
