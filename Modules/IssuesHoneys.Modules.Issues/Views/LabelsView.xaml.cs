using IssuesHoneys.Modules.Issues.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void PART_LabelsListViewItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is TextBlock)
            {
                var context = this.DataContext as LabelsViewModel;
                var item = e.OriginalSource as TextBlock;
                context.SelectedLabel = item.DataContext as BusinessTypes.Label;
            }
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focus();
        }

    }
}
