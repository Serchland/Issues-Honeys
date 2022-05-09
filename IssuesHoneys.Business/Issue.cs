using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace IssuesHoneys.Business
{
    public class Issue : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        private string _crtnUser;
        public string CrtnUser
        {
            get { return _crtnUser; }
            set { SetProperty(ref _crtnUser, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set { SetProperty(ref _body, value); }
        }

        private DateTime _lastUpdDate;
        public DateTime LastUpdDate
        {
            get { return _lastUpdDate; }
            set { SetProperty(ref _lastUpdDate, value); }
        }

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private int _totalComments;
        public int TotalComments
        {
            get { return _totalComments; }
            set { SetProperty(ref _totalComments, value); }
        }

        private int _closedBy;
        public int ClosedBy
        {
            get { return _closedBy; }
            set { SetProperty(ref _closedBy, value); }
        }

        private DateTime _closedDate;
        public DateTime ClosedDate
        {
            get { return _closedDate; }
            set { SetProperty(ref _closedDate, value); }
        }

        private List<int> _assignees;
        public List<int> Assignees
        {
            get { return _assignees; }
            set { SetProperty(ref _assignees, value); }
        }

        private List<Label> _labels;
        public List<Label> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        private List<int> _projects;
        public List<int> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        private List<Milestone> _milestones;
        public List<Milestone> Milestones
        {
            get { return _milestones; }
            set { SetProperty(ref _milestones, value); }
        }
    }

    public enum State
    {
        IsOpen,
        IsClosed
    }

    public class Label : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
    }

    public class Milestone : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { SetProperty(ref _dueDate, value); }
        }

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        private string _crtnUser;
        public string CrtnUser
        {
            get { return _crtnUser; }
            set { SetProperty(ref _crtnUser, value); }
        }
    }
}
