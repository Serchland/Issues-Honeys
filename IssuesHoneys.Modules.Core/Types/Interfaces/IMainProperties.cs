using IssuesHoneys.Business.Types;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace IssuesHoneys.Core.Types.Interfaces
{
    public interface IMainProperties
    {
        ObservableCollection<Label> Labels { get; set; }
        ObservableCollection<Issue> Issues { get; set; }
        ObservableCollection<Milestone> Milestones { get; set; }
        ObservableCollection<User> Users { get; set; }
    }

    public class MainProperties : IMainProperties
    {
        public ObservableCollection<Label> Labels { get; set; }
        public ObservableCollection<Issue> Issues { get; set; }
        public ObservableCollection<Milestone> Milestones { get; set; }
        public ObservableCollection<User> Users { get; set; }
    }
}
