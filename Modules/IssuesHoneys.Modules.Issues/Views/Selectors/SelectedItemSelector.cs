using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IssuesHoneys.Modules.Issues.Views.Selectors
{
    internal class SelectedItemSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Task)
            {
                //if (taskitem.Priority == 1)
                //    return
                //        element.FindResource("importantTaskTemplate") as DataTemplate;
                //else
                //    return
                //        element.FindResource("myTaskTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
