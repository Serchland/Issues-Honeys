using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
