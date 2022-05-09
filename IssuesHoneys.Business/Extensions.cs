using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IssuesHoneys.Business
{
    public static class Extensions
    {
        public static Brush ToBrush(this string HexColorString)
        {
            {
                return (Brush)new BrushConverter().ConvertFromString(HexColorString);
            }
        }
    }
}