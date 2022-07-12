using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Business.Types
{
    public class NavigationParameters<T>
    {
      
        public string Destination{ get; set; }
        List<T> Parameters { get; set; }
    }
}
