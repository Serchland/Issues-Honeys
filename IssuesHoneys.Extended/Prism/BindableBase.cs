using System.Runtime.CompilerServices;

namespace IssuesHoneys.Extended.Prism
{
    public abstract class BindableBase : INotifyPropertyChangedEnhanced
    {
        public event PropertyChangedEventHandlerEnhanced PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            var oldValue = storage;
            storage = value;
            this.OnPropertyChanged(oldValue, value, propertyName);
            return true;
        }
        protected void OnPropertyChanged<T>(T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedExtendedEventArgs(propertyName, oldValue, newValue));
        }
    }
}