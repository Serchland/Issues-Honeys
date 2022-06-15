using IssuesHoneys.Extended.MyPrism.EventArgs;
using IssuesHoneys.Extended.MyPrism.Interfaces;
using System.Runtime.CompilerServices;

namespace IssuesHoneys.Extended.MyPrism.Mvvm
{
    public abstract class BindableBase : PrismBindeableBase, INotifyPropertyChangedEnhanced
    {
        public new event PropertyChangedEventHandlerEnhanced PropertyChanged;
        protected new bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
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
            base.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
    