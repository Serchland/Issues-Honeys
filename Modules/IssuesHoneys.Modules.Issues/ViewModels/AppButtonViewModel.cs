using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : PartViewModelBase<string>
    {
        public AppButtonViewModel(IApplicationCommands applicationCommands) : base(applicationCommands)
        {
        }
    }
}
