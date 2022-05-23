using IssuesHoneys.Modules.Issues.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void PART_LabelsListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //SERCH00: Does the trick, but it is improvable 
            var context = this.DataContext as LabelsViewModel;
            
            if (context.SelectedLabel == null)
            {
                var item = e.OriginalSource as ListViewItem;
                context.MouserEnterLabel = (BusinessTypes.Label)item.Content;
                item.MouseEnter -= new MouseEventHandler(PART_LabelsListViewItem_MouseEnter);
            }
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.Focus();
        }

    }
}
