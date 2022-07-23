using IssuesHoneys.Modules.Issues.ViewModels;
using System.Windows.Controls;

namespace IssuesHoneys.Modules.Issues.Views
{
    /// <summary>
    /// Lógica de interacción para AppMain.xaml
    /// </summary>
    public partial class IssuesView : UserControl
    {
        public IssuesView()
        {
            InitializeComponent();
        }

        private void PART_IssuesListViewItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBlock editTextblock && editTextblock.Name == "PART_TextBlockDetails" || e.OriginalSource is TextBlock deleteTextblock && deleteTextblock.Name == "PART_TextBlocklDelete")
            {
                var context = this.DataContext as IssuesViewModel;
                var item = e.OriginalSource as TextBlock;
                context.SelectedItem = item.DataContext as Business.Types.Issue;
            }
        }
    }
}
