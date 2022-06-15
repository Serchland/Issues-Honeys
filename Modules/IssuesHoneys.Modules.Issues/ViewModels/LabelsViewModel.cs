using IssuesHoneys.BusinessTypes;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.Generic;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class LabelsViewModel : ViewModelBase
    {
        private IIssueService _isuesService;
        public LabelsViewModel(IApplicationCommands applicationsCommands, IIssueService issueService) : base(applicationsCommands)
        {
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {

            _labels = new ObservableCollection<Label>(_isuesService.GetLabels());
            _labelsView = CollectionViewSource.GetDefaultView(_labels);
            _newLabel = new Label(Brushes.Gray);
            _newLabelViewVisibility = Visibility.Collapsed;
            _totalLabels = _labels.Count.ToString();
            _labelsView.Filter = LabelsFilter;
        }

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

        #region "Properties"

        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get
            {
                return _labels;
            }
            set
            {
                SetProperty(ref _labels, value);
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

        private Label _selectedLabel;
        public Label SelectedLabel
        {
            get
            {
                return _selectedLabel;
            }
            set
            {
                if (_selectedLabel != null)
                    _selectedLabel.IsEdditing = false;

                SetProperty(ref _selectedLabel, value);
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

        #region "Commands"

        private DelegateCommand _createLabelCommand;
        public DelegateCommand CreateLabelCommand =>
            _createLabelCommand ?? (_createLabelCommand = new DelegateCommand(ExecuteCreateLabelCommand));

        void ExecuteCreateLabelCommand()
        {
            if (_newLabel == null)
                throw new ArgumentException(ArgumentExceptionMessage);

            _isuesService.CreateLabel(NewLabel);
            Labels.Add(NewLabel);
            NewLabelViewVisibilitity = Visibility.Collapsed;
        }

        private DelegateCommand _deleteLabelCommand;
        public DelegateCommand DeleteLabelCommand =>
            _deleteLabelCommand ?? (_deleteLabelCommand = new DelegateCommand(ExecuteDeleteLabelCommand));

        void ExecuteDeleteLabelCommand()
        {

            if (_selectedLabel == null)
                throw new ArgumentException(ArgumentExceptionMessage);


        }

        
            private DelegateCommand _filterLabelsCommand;
        public DelegateCommand FilterLabelsCommand =>
            _filterLabelsCommand ?? (_filterLabelsCommand = new DelegateCommand(ExecuteFilterLabelsCommand));

        void ExecuteFilterLabelsCommand()
        {
            LabelsView.Refresh();
        }

        private DelegateCommand _updateLabelCommand;
        public DelegateCommand UpdateCommand =>
            _updateLabelCommand ?? (_updateLabelCommand = new DelegateCommand(ExecuteUpdateLabelCommand));

        void ExecuteUpdateLabelCommand()
        {

            if (_selectedLabel == null)
                throw new ArgumentException(ArgumentExceptionMessage);

            _isuesService.UpdateLabel(SelectedLabel);
            SelectedLabel.IsEdditing = false;
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
                SelectedLabel.Color = result;

        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        void ExecuteCancelCommand()
        {
            SelectedLabel.GetOldValue(OldLabelValue);
            SelectedLabel.IsEdditing = false;

            OldLabelValue = null;
        }

        private DelegateCommand _isEdditingCommand;
        public DelegateCommand IsEdditingCommand =>
            _isEdditingCommand ?? (_isEdditingCommand = new DelegateCommand(ExecuteIsEdditingCommand));

        void ExecuteIsEdditingCommand()
        {
            OldLabelValue = SelectedLabel.Clone() as Label;
            SelectedLabel.IsEdditing = true;
        }
        #endregion
    }
}
