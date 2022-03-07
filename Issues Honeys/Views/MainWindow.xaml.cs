using Fluent;
using IssuesHoneys.Core.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System.Windows;

namespace Issues_Honeys.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private IApplicationCommands _applicationCommands;
        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();
            _applicationCommands = applicationCommands;
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _applicationCommands.NavigationNavigateCommand.Execute("");
        }
    }
}
