using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppButtonViewModel : AppButtonBase 
    {
        public AppButtonViewModel(IApplicationCommands applicationCommand) : base(applicationCommand)
        {
        }
    }
}
