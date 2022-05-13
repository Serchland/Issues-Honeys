using Prism.Mvvm;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppFooterViewModel : BindableBase
    {
        public AppFooterViewModel()
        {
            Message = "Issues Footer under construction";
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}
