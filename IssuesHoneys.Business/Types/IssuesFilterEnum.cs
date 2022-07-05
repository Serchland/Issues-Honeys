using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Business.Types
{
    public enum IssuesFilterEnum
    {
        NoMillestones = -2,
        Unlabeled = -1,
        Authors = 0,
        Labels = 1,
        Projects = 2,
        Millestones = 3,
        Assignee = 4
    }
}
