using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;

namespace IssuesHoneys.BusinessTypes
{
    /// <summary>
    /// Extension methods used by the application
    /// </summary>
    public static class Extensions 
    {
        public static T OriginalLabelValueClone<T>(this ListViewItem item)
        {
            object value = item.DataContext;

            using (var ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, value);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}