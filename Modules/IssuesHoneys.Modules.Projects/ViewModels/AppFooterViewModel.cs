using Prism.Mvvm;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppFooterViewModel : BindableBase
    {
        public AppFooterViewModel()
        {
            Message = "Projects Footer under construction";
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}
