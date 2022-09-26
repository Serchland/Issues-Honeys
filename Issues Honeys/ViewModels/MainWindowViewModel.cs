using Prism.Mvvm;

namespace Issues_Honeys.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //IRegionManager _regionManager;

        public MainWindowViewModel() 
        {
            //_regionManager = regionManager;
            //applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);

            _buttonContentRegionHeight = 73;
        }

        //SERCH00: Manage height in the button directly
        private double _buttonContentRegionHeight;
        public double ButtonContentRegionHeight
        {
            get { return _buttonContentRegionHeight; }
            set { SetProperty(ref _buttonContentRegionHeight, value); }
        }

        //private DelegateCommand<string> _navigateCommand;
        //public DelegateCommand<string> NavigateCommand =>
        //    _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        //void ExecuteNavigateCommand(string param)
        //{
        //    //SERCH00: only for test pourpoposes:
        //    //string res = Application.Current.FindResource("WaterMarkSearchLabelsCaption").ToString();
        //    if (string.IsNullOrEmpty(param))
        //        throw new ArgumentNullException(ArgumentExceptionMessage);
                 
        //    switch (param.Split(';').FirstOrDefault())
        //    {
        //        case CommandParameters.Details:
        //            var navParam = new NavigationParameters();
        //            navParam.Add(BookMark.Id, param.Split(';').LastOrDefault());

        //            _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssueDetails, navParam);

        //            ButtonContentRegionHeight = 0;
        //            break;

        //        case CommandParameters.Issues:
        //            _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

        //            ButtonContentRegionHeight = 73;
        //            break;

        //        case CommandParameters.Labels:
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.LabelsMain);

        //            break;

        //        case CommandParameters.Milestones:
        //        case CommandParameters.CreateMilestone:
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.MilestonesMain);

        //            break;

        //        case CommandParameters.NewIssue:
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewIssue);

        //            break;

        //        case CommandParameters.NewMilestone:
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.NewMilestone);

        //            break;

        //        case CommandParameters.Projects:
        //            _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.ProjectFooter);
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.ProjectMain);

        //            break;

        //        default:
        //            _regionManager.RequestNavigate(RegionNames.FooterContentRegion, RegisterForNavigation.IssueFooter);
        //            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegisterForNavigation.IssuesMain);

        //            break;
        //    }
        //}
    }
}
