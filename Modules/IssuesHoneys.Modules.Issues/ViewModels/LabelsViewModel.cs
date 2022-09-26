using IssuesHoneys.Business.Types;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class LabelsViewModel : ViewModelBase<Label>
    {
        private IIssueService _issuesService;
        private IMainProperties _mainProperties;
        public LabelsViewModel(IMainProperties mainProperties, IIssueService issueService, IRegionManager regionManager, IApplicationCommands applicationsCommands) : base(regionManager, applicationsCommands)
        {
            _mainProperties = mainProperties;
            _issuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            if (_mainProperties.Labels == null)
                Labels = new ObservableCollection<Label>(_issuesService.GetLabels(LabelType.Issue));

            LabelsView = CollectionViewSource.GetDefaultView(Labels);
            NewLabel = new Label(Brushes.Gray);
            NewLabelViewVisibilitity = Visibility.Collapsed;
            TotalLabels = Labels.Count.ToString();
            LabelsView.Filter = LabelsFilter;
            LabelsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            _sortItems = GetSortItems();
        }

        #region "Commands"
        private DelegateCommand<LabelSortEnum?> _selectedSortItemCommand;
        public DelegateCommand<LabelSortEnum?> SelectedSortItemCommand =>
            _selectedSortItemCommand ?? (_selectedSortItemCommand = new DelegateCommand<LabelSortEnum?>(ExecuteSelectedSortItemCommand));

        void ExecuteSelectedSortItemCommand(LabelSortEnum? param)
        {
            _labelsView.SortDescriptions.Clear();
            switch (param)
            {
                case LabelSortEnum.Alphabetically:
                    _labelsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    break;

                case LabelSortEnum.ReverseAlphabetically:
                    _labelsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                    break;

                case LabelSortEnum.FewestIssues:
                    _labelsView.SortDescriptions.Add(new SortDescription("TotalIssues", ListSortDirection.Ascending));
                    break;

                case LabelSortEnum.MostIssues:
                    _labelsView.SortDescriptions.Add(new SortDescription("TotalIssues", ListSortDirection.Descending));
                    break;
            }
        }

        private DelegateCommand _createLabelCommand;
        public DelegateCommand CreateLabelCommand =>
            _createLabelCommand ?? (_createLabelCommand = new DelegateCommand(ExecuteCreateLabelCommand));

        void ExecuteCreateLabelCommand()
        {
            if (_newLabel == null)
                throw new ArgumentException(ArgumentExceptionMessage);

            _issuesService.CreateLabel(NewLabel);
            Labels = new ObservableCollection<Label>(_issuesService.GetLabels(LabelType.Issue));
            NewLabelViewVisibilitity = Visibility.Collapsed;
        }

        private DelegateCommand _deleteLabelCommand;
        public DelegateCommand DeleteLabelCommand =>
            _deleteLabelCommand ?? (_deleteLabelCommand = new DelegateCommand(ExecuteDeleteLabelCommand));

        void ExecuteDeleteLabelCommand()
        {

            if (_selectedItem == null)
                throw new ArgumentException(ArgumentExceptionMessage);

            _issuesService.DeleteLabel(SelectedItem.Id);
            Labels = new ObservableCollection<Label>(_issuesService.GetLabels(LabelType.Issue));
            CollectionViewSource.GetDefaultView(Labels).Refresh();
        }

        private DelegateCommand _filterLabelsCommand;
        public DelegateCommand FilterLabelsCommand =>
            _filterLabelsCommand ?? (_filterLabelsCommand = new DelegateCommand(ExecuteFilterLabelsCommand));

        void ExecuteFilterLabelsCommand()
        {
            CollectionViewSource.GetDefaultView(Labels).Refresh();
        }

        private DelegateCommand _updateLabelCommand;
        public DelegateCommand UpdateCommand =>
            _updateLabelCommand ?? (_updateLabelCommand = new DelegateCommand(ExecuteUpdateLabelCommand));

        void ExecuteUpdateLabelCommand()
        {

            if (_selectedItem == null)
                throw new ArgumentException(ArgumentExceptionMessage);

            _issuesService.UpdateLabel(SelectedItem);
            Labels = new ObservableCollection<Label>(_issuesService.GetLabels(LabelType.Issue));
            SelectedItem.IsEdditing = false;
        }

        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentException(ArgumentExceptionMessage);

            switch (parameter)
            {
                case CommandParameters.Cancel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case CommandParameters.NewLabel:
                    NewLabel = new Label(Brushes.Gray);
                    NewLabelViewVisibilitity = Visibility.Visible;
                    break;
            }
        }

        private DelegateCommand<string> _randomColor;
        public DelegateCommand<string> RandomColorCommand =>
            _randomColor ?? (_randomColor = new DelegateCommand<string>(ExecuteRandomColorCommand));

        private void ExecuteRandomColorCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException(ArgumentExceptionMessage);

            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);

            result = (Brush)properties[random].GetValue(null, null);
            if (parameter == CommandParameters.Create)
                NewLabel.Color = result;
            else
                SelectedItem.Color = result;

        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        void ExecuteCancelCommand()
        {
            SelectedItem.GetOldValue(OldLabelValue);
            SelectedItem.IsEdditing = false;

            OldLabelValue = null;
        }

        private DelegateCommand _isEdditingCommand;
        public DelegateCommand IsEdditingCommand =>
            _isEdditingCommand ?? (_isEdditingCommand = new DelegateCommand(ExecuteIsEdditingCommand));

        void ExecuteIsEdditingCommand()
        {
            OldLabelValue = SelectedItem.Clone() as Label;
            SelectedItem.IsEdditing = true;
        }
        #endregion

        #region "Methods"
        private bool LabelsFilter(object item)
        {
            Label label = item as Label;
            if (!String.IsNullOrEmpty(FilterText))
            {
                return label.Name.ToLower().Contains(FilterText.ToLower());
            }
            else
            {
                return true;
            }
        }

        private ObservableCollection<LabelSortDto> GetSortItems()
        {
            ObservableCollection<LabelSortDto> result = new ObservableCollection<LabelSortDto>()
                { 
                    new LabelSortDto(){StringValue =  Application.Current.Resources["SortAlphabetically"].ToString(), EnumValue = LabelSortEnum.Alphabetically}   ,
                    new LabelSortDto(){StringValue =  Application.Current.Resources["SortReverseAlphabetically"].ToString(), EnumValue = LabelSortEnum.ReverseAlphabetically}   ,
                    new LabelSortDto(){StringValue =  Application.Current.Resources["SortMostIssues"].ToString(), EnumValue = LabelSortEnum.MostIssues}   ,
                    new LabelSortDto(){StringValue =  Application.Current.Resources["SortFewestAlphabetically"].ToString(), EnumValue = LabelSortEnum.FewestIssues}   ,
                };

            return result;
        }
        #endregion

        #region "Properties"
        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get
            {
                return _mainProperties.Labels;
            }
            set
            {
                _mainProperties.Labels = value;
                SetProperty(ref _labels, value);
            }
        }

        private ObservableCollection<LabelSortDto> _sortItems;
        public ObservableCollection<LabelSortDto> SortItems
        {
            get
            {
                return _sortItems;
            }
        }

        private LabelSortDto _selectedLabelSort;
        public LabelSortDto SelectedLabelSort
        {
            get
            {
                return _selectedLabelSort;
            }
            set
            {
                SetProperty(ref _selectedLabelSort, value);
            }
        }

        private ICollectionView _labelsView;
        public ICollectionView LabelsView
        {
            get
            {
                return _labelsView;
            }
            set
            {
                SetProperty(ref _labelsView, value);
            }
        }

        private Label _newLabel;
        public Label NewLabel
        {
            get
            {
                return _newLabel;
            }
            set
            {
                SetProperty(ref _newLabel, value);
            }
        }

        private string _filterText;
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                SetProperty(ref _filterText, value);
            }
        }

        private Label _oldLabelValue;
        public Label OldLabelValue
        {
            get
            {
                return _oldLabelValue;
            }
            set
            {
                SetProperty(ref _oldLabelValue, value);
            }
        }

        //private Label _selectedItem;
        public override Label SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != null)
                    _selectedItem.IsEdditing = false;

                SetProperty(ref _selectedItem, value);
            }
        }

        private string _totalLabels;
        public string TotalLabels
        {
            get
            {
                return _totalLabels;
            }
            set
            {
                SetProperty(ref _totalLabels, value);
            }
        }

        private Visibility _newLabelViewVisibility;
        public Visibility NewLabelViewVisibilitity
        {
            get
            {
                return _newLabelViewVisibility;
            }
            set
            {
                SetProperty(ref _newLabelViewVisibility, value);
            }
        }
        #endregion

    }
}
