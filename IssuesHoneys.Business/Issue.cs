using Prism.Mvvm;
using System;
using System.Collections.Generic;

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

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
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

        private List<int> _millestones;
        public List<int> Millestones
        {
            get { return _millestones; }
            set { SetProperty(ref _millestones, value); }
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

        private string _associatedIssues;
        public string AssociatedIssues
        {
            get { return _associatedIssues; }
            set { SetProperty(ref _associatedIssues, value); }
        }
    }

    public class Millestone : BindableBase
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

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { SetProperty(ref _dueDate, value); }
        }
    }
}
