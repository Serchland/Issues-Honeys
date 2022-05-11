using Prism.Commands;

namespace IssuesHoneys.Core.Types.Interfaces
{
    /// <summary>
    /// PRISM commands used by the application
    /// </summary>
    public interface IApplicationCommands
    {
        CompositeCommand NavigateCommand { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}
