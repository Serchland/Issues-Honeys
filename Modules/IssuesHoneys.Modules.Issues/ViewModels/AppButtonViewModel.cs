using IssuesHoneys.Core.NameDefinition.Captions;
using Prism.Mvvm;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class AppButtonViewModels : BindableBase
    {
        public AppButtonViewModels() 
        {
            _caption = Captions.IssuesButtonCaption;
        }

        private readonly string _caption;
        public string Caption
        {
            get { return _caption; }
        }
    }
}
