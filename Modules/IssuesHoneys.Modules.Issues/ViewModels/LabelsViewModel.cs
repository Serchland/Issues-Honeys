using IssuesHoneys.BusinessTypes;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.ViewModels
{
    public class LabelsViewModel : ViewModelBase
    {
        private IIssueService _isuesService;
        public LabelsViewModel(IApplicationCommands applicationsCommands, IIssueService issueService) : base (applicationsCommands)
        {
            _isuesService = issueService;
            Initialize();
        }

        private void Initialize()
        {
            _labels = new ObservableCollection<Label>(_isuesService.GetLabels());
            _newLabelViewVisibility = Visibility.Collapsed;
            _totalLabels = _labels.Count.ToString();
            _brush = Brushes.Gray;
        }

        #region "Properties"
        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        private string _brushString;
        public string BrushString
        {
            get { return _brushString; }
            set { SetProperty(ref _brushString, value); }
        }

        private Brush _brush;
        public Brush Brush
        {
            get { return _brush; }
            set
            {
                BrushString = _brush.ToString();
                SetProperty(ref _brush, value);
            }
        }

        private string _totalLabels;
        public string TotalLabels
        {
            get { return _totalLabels; }
            set { SetProperty(ref _totalLabels, value); }
        }

        private Visibility _newLabelViewVisibility;
        public Visibility NewLabelViewVisibilitity
        {
            get { return _newLabelViewVisibility; }
            set { SetProperty(ref _newLabelViewVisibility, value); }
        }
        #endregion

        #region "Commands"
        private DelegateCommand<string> _newLabelVisibilityCommand;
        public DelegateCommand<string> NewLabelVisibilityCommand =>
            _newLabelVisibilityCommand ?? (_newLabelVisibilityCommand = new DelegateCommand<string>(ExecuteNewLabelVisibilityCommand));

        void ExecuteNewLabelVisibilityCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentException("parameter cant be null");

            switch (parameter)
            {
                case CommandParameters.Cancel:
                    NewLabelViewVisibilitity = Visibility.Collapsed;
                    break;

                case CommandParameters.NewLabel:
                    NewLabelViewVisibilitity = Visibility.Visible;
                    break;
            }
        }

        private DelegateCommand _randomColor;
        public DelegateCommand RandomColor =>
            _randomColor ?? (_randomColor = new DelegateCommand(ExecuteRandomColor));

        private void ExecuteRandomColor()
        {
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            Brush = (Brush)properties[random].GetValue(null, null);
        }

        private DelegateCommand<string> _cancelCommand;
        public DelegateCommand<string> CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand<string>(ExecuteCommandName));

        void ExecuteCommandName(string parameter)
        {

        }
        #endregion
    }
}
