using IssuesHoneys.Core.Types.Base;
using IssuesHoneys.Core.Types.Interfaces;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppRibbonTabButtonViewModel : AppRibbonTabButtonBase 
    {
        public AppRibbonTabButtonViewModel(IApplicationCommands applicationCommand) : base(applicationCommand)
        {
        }
    }
}
