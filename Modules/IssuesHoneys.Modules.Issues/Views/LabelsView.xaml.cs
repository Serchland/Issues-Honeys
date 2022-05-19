using System.Windows.Controls;

namespace IssuesHoneys.Modules.Issues.Views
{
    /// <summary>
    /// Lógica de interacción para AppLabel.xaml
    /// </summary>
    public partial class LabelsView : UserControl
    {
        public LabelsView()
        {
            InitializeComponent();
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focus();
        }

        private void NewLabel_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
