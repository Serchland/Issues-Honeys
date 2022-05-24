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
    /// Lógica de interacción para EditLabel.xaml
    /// </summary>
    public partial class EditLabel : UserControl
    {
        public EditLabel()
        {
            InitializeComponent();

            //SERCH00: Under consideration
            //this.Loaded += EditLabel_Loaded;
        }

        //private void EditLabel_Loaded(object sender, RoutedEventArgs e)
        //{
        //    PART_LabelName.Focus();
        //    this.Loaded -= EditLabel_Loaded;
        //}
    }
}
