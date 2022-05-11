using System.Windows;
using System.Windows.Controls;

namespace IssuesHoneys.Core.Views
{
    /// <summary>
    /// Lógica de interacción para ButtonLabels.xaml
    /// </summary>
    public partial class ButtonLabels : Button
    {
        public ButtonLabels()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.RegisterAttached("TotalItems", typeof(string), typeof(ButtonLabels), new FrameworkPropertyMetadata("0"));

        public static string GetTotalItems(UIElement element)
        {
            return element.GetValue(TotalItemsProperty).ToString();
        }

        public static void SetTotalItems(UIElement element, string value)
        {
            element.SetValue(TotalItemsProperty, value);
        }

        
    }
}
