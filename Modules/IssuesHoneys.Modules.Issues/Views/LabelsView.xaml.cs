using IssuesHoneys.Modules.Issues.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IssuesHoneys.Modules.Issues.Views
{
    /// <summary>
    /// Lógica de interacción para AppLabel.xaml
    /// </summary>
    public partial class LabelsView : UserControl
    {
        public LabelsView(LabelsViewModel context)
        {
            InitializeComponent();
            //CollectionViewSource res = GridLabels.Resources["LabelsFilteredCollectionKey"] as CollectionViewSource;

            //res.Source = context.Labels;
        }

        private void PART_LabelsListViewItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is TextBlock editTextblock && editTextblock.Name == "PART_TextBlocklEdit" || e.OriginalSource is TextBlock deleteTextblock && deleteTextblock.Name == "PART_TextBlocklDelete")
            {
                var context = this.DataContext as LabelsViewModel;
                var item = e.OriginalSource as TextBlock;
                context.SelectedLabel = item.DataContext as Business.Types.Label;
            }
        }

        private void PART_LabelsListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            //var context = this.DataContext as LabelsViewModel;
            //var item = e.OriginalSource as ListViewItem;

            //context.SelectedOriginalLabel = item.DataContext as BusinessTypes.Label;
            //context.SelectedOriginalLabel = item.OriginalLabelValueClone<ListViewItem>;
        }
        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focus();
        }

    }
}
