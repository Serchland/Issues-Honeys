using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class IssueDetailsViewModel : PartViewModelBase<Issue>, INavigationAware
    {
        private IIssueService _issueService;
        public IssueDetailsViewModel(IApplicationCommands applicationCommands, IIssueService issueService) : base(applicationCommands)
        {
            _issueService = issueService;
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string detailId = (string)navigationContext.Parameters[BookMark.Id];

            SelectedItem = _issueService.GetIssuesById(Convert.ToInt32(detailId));
        }
    }
}
