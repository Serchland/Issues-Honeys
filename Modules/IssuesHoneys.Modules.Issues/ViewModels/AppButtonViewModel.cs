using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.Types.Interfaces;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModel : ViewModelBase<object>
    {
        public AppButtonViewModel(IApplicationCommands applicationCommands) : base(applicationCommands)
        {
        }
    }
}
