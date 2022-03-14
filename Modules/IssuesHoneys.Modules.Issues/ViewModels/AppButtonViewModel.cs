using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : ViewModelBase
    {
        public AppButtonViewModel(IApplicationCommands applicationCommand) : base(applicationCommand)
        {
        }
    }
}
