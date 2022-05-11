using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class NewMilestoneViewModel : ViewModelBase
    {
        public NewMilestoneViewModel(IApplicationCommands applicationCommands) : base(applicationCommands)
        {
        }
    }
}
