using Prism.Commands;

namespace IssuesHoneys.Core.Types.Interfaces
{
    public interface IApplicationCommands
    {
        CompositeCommand NavigationNavigateCommand { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand NavigationNavigateCommand { get; } = new CompositeCommand();
    }
}
