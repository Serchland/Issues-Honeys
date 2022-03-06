using IssuesHoneys.Core.NameDefinition.Captions;
using Prism.Mvvm;

namespace IssuesHoneys.Modules.Projects.ViewModels
{
    public class AppButtonViewModel : BindableBase
    {
        public AppButtonViewModel()
        {
            _caption = Captions.ProjectsButtonCaption;
        }

        private string _caption;
        public string Caption
        {
            get { return _caption; }
        }
    }
}
