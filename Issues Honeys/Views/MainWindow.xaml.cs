using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using MahApps.Metro.Controls;

namespace Issues_Honeys.Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IApplicationCommands _applicationCommands;

        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();
            _applicationCommands = applicationCommands;
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _applicationCommands.NavigationNavigateCommand.Execute(ModuleNameParams.Issues);
        }
    }
}
