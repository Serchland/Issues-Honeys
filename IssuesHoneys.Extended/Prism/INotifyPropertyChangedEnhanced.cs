namespace IssuesHoneys.Extended.Prism
{
    public delegate void PropertyChangedEventHandlerEnhanced(object sender, PropertyChangedExtendedEventArgs e);

    internal interface INotifyPropertyChangedEnhanced
    {
        event PropertyChangedEventHandlerEnhanced PropertyChanged;
    }
}
