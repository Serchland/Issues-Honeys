using IssuesHoneys.Extended.MyPrism.EventArgs;

namespace IssuesHoneys.Extended.MyPrism.Interfaces
{
    public delegate void PropertyChangedEventHandlerEnhanced(object sender, PropertyChangedExtendedEventArgs e);
    public interface INotifyPropertyChangedEnhanced
    {
        event PropertyChangedEventHandlerEnhanced PropertyChanged;
    }
}