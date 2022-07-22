using IssuesHoneys.Business.Types;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Core.Types.Interfaces
{
    public interface IMainDatas
    {
        ObservableCollection<Milestone> Milestones { get;  set; }
        //ObservableCollection<Label> Labels { get; }
        //ObservableCollection<User> Users { get; }

    }

    public class MainDatas : BindableBase, IMainDatas
    {
        public MainDatas()
        {
            
        }
        //public MainDatas(IIssueService issuesService)
        //{
        //    Milestones = new ObservableCollection<Milestone>(issuesService.GetMilestones());
        //}
        private ObservableCollection<Milestone> _milestones;
        private ObservableCollection<Label> _labels;
        //public ObservableCollection<Milestone> Milestones
        //{
        //    get { return _milestones; }
        //    set { SetProperty(ref _milestones, value); }
        //}
    }
}
