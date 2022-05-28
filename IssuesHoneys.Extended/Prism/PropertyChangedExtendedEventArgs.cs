using System.ComponentModel;

namespace IssuesHoneys.Extended.Prism
{
    public class PropertyChangedExtendedEventArgs : PropertyChangedEventArgs
    {
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public PropertyChangedExtendedEventArgs(string propertyName, object oldValue, object newValue) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
