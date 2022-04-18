using System.Windows.Controls;

namespace IssuesHoneys.Modules.Issues.Views
{
    /// <summary>
    /// Lógica de interacción para Milestone.xaml
    /// </summary>
    public partial class MilestonesView : UserControl
    {
        public MilestonesView()
        {
            InitializeComponent();
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focus();
        }
    }
}
