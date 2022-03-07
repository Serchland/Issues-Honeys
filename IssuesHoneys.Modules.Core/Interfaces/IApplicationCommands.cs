using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Core.Interfaces
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
