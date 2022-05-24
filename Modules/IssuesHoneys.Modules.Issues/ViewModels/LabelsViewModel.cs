﻿using IssuesHoneys.BusinessTypes;
using IssuesHoneys.Core.Base;
using IssuesHoneys.Core.NameDefinition;
using IssuesHoneys.Core.Types.Interfaces;
using IssuesHoneys.Services.Interfaces;
using Prism.Commands;
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
            _newLabel = new Label(Brushes.Gray);
            _newLabelViewVisibility = Visibility.Collapsed;
            _totalLabels = _labels.Count.ToString();
        }

        #region "Properties"

        

        private ObservableCollection<Label> _labels;
        public ObservableCollection<Label> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        private Label _newLabel;
        public Label NewLabel
        {
            get
            { return _newLabel;}
            set
            { SetProperty(ref _newLabel, value);}
        }

        //private Label _mouseEnterLabel;
        //public Label MouserEnterLabel
        //{
        //    get
        //    {
        //        return _mouseEnterLabel;
        //    }
        //    set
        //    {
        //        SetProperty(ref _mouseEnterLabel, value);
        //    }
        //}

        private Label _selectedLabel;
        public Label SelectedLabel
        {
            get
            { return _selectedLabel; }
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

            if (parameter == CommandParameters.Create)
                NewLabel.Color = (Brush)properties[random].GetValue(null, null);
            else
                SelectedLabel.Color = (Brush)properties[random].GetValue(null, null);
        }

        private DelegateCommand<string> _cancelCommand;
        public DelegateCommand<string> CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand<string>(ExecuteCancelCommand));

        void ExecuteCancelCommand(string parameter)
        {

        }

        private DelegateCommand _isEdditingCommand;
        public DelegateCommand IsEdditingCommand =>
            _isEdditingCommand ?? (_isEdditingCommand = new DelegateCommand(ExecuteIsEdditingCommand));

        void ExecuteIsEdditingCommand()
        {
            SelectedLabel.IsEdditing = true;
        }
        #endregion
    }
}
