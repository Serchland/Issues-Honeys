using System.Windows;
using System.Windows.Controls;

namespace IssuesHoneys.Core.Views
{
    /// <summary>
    /// Lógica de interacción para ButtonsMillestones.xaml
    /// </summary>
    public partial class ButtonMilestones : Button
    {
        public ButtonMilestones()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.RegisterAttached("TotalItems", typeof(string), typeof(ButtonMilestones), new FrameworkPropertyMetadata("0"));

        public static void SetTotalItems(UIElement element, string value)
        {
            element.SetValue(TotalItemsProperty, value);
        }

        public static string GetTotalItems(UIElement element)
        {
            return element.GetValue(TotalItemsProperty).ToString();
        }
    }
}
