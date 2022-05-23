using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace IssuesHoneys.BusinessTypes
{
    #region "ISSUE"
    /// <summary>
    /// Class used to represent a ISSUE
    /// </summary>
    public class Issue : BindableBase
    {
        /// <summary>
        /// USERS assignees to the ISSUE
        /// </summary>
        private List<User> _assignees;
        public List<User> Assignees
        {
            get { return _assignees; }
            set { SetProperty(ref _assignees, value); }
        }

        /// <summary>
        /// Body of the ISSUE
        /// </summary>
        private string _body;
        public string Body
        {
            get { return _body; }
            set { SetProperty(ref _body, value); }
        }

        /// <summary>
        /// USER who close the ISSUE
        /// </summary>
        private int _closedBy;
        public int ClosedBy
        {
            get { return _closedBy; }
            set { SetProperty(ref _closedBy, value); }
        }

        /// <summary>
        /// Date on which the ISSUE was closed
        /// </summary>
        private DateTime _closedDate;
        public DateTime ClosedDate
        {
            get { return _closedDate; }
            set { SetProperty(ref _closedDate, value); }
        }

        /// <summary>
        /// Date on which the ISSUE was created
        /// </summary>
        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        /// <summary>
        /// USER who created the ISSUE
        /// </summary>
        private string _crtnUser;
        public string CrtnUser
        {
            get { return _crtnUser; }
            set { SetProperty(ref _crtnUser, value); }
        }

        /// <summary>
        /// ISSUE identifier
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// Date of last update
        /// </summary>
        private DateTime _lastUpdDate;
        public DateTime LastUpdDate
        {
            get { return _lastUpdDate; }
            set { SetProperty(ref _lastUpdDate, value); }
        }

        /// <summary>
        /// LABELS assigned to the ISSUE
        /// </summary>
        private List<Label> _labels;
        public List<Label> Labels
        {
            get { return _labels; }
            set { SetProperty(ref _labels, value); }
        }

        /// <summary>
        /// MILESTONES assigned to the ISSUE
        /// </summary>
        private List<Milestone> _milestones;
        public List<Milestone> Milestones
        {
            get { return _milestones; }
            set { SetProperty(ref _milestones, value); }
        }

        /// <summary>
        /// Number of the ISSUE
        /// </summary>
        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        /// <summary>
        /// PROJECTS assigned to the ISSUE
        /// </summary>
        private List<int> _projects;
        public List<int> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        /// <summary>
        /// State of the ISSUE. Values: Open (0)/ Closed(1)
        /// </summary>
        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        /// <summary>
        /// Title of the ISSUE
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Total comments in the ISSUE. Calculated by SQL function
        /// </summary>
        private int _totalComments;
        public int TotalComments
        {
            get { return _totalComments; }
            set { SetProperty(ref _totalComments, value); }
        }
    }
    #endregion

    #region "LABEL"
    /// <summary>
    /// Class used to represent a LABEL
    /// </summary>
    public class Label : BindableBase
    {
        public Label()
        {

        }
        public Label(Brush color)
        {
            _color = color;
            _brushString = color.ToString();
        }

        private string _brushString;
        public string BrushString
        {
            get { return _brushString; }
            set { SetProperty(ref _brushString, value); }
        }

        /// <summary>
        /// Date on which the LABEL was created
        /// </summary>
        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        /// <summary>
        /// USER who created the LABEL
        /// </summary>
        private string _crtnUser;
        public string CrtnUser
        {
            get { return _crtnUser; }
            set { SetProperty(ref _crtnUser, value); }
        }

        /// <summary>
        /// Color of the LABEL. Used to set the background colour of the LABEL
        /// </summary>
        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set 
            {   
                SetProperty(ref _color, value);
                BrushString = _color.ToString();
            }
        }

        /// <summary>
        /// LABEL description
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        
        /// <summary>
        /// LABEL identifier
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// LABEL name
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _totalIssuesWithLabel;
        public int TotalIssuesWithLabel
        {
            get { return _totalIssuesWithLabel; }
            set { _totalIssuesWithLabel = value; }
        }

        /// <summary>
        /// Extended property
        /// </summary>
        private bool _isEdditing;
        public bool IsEdditing
        {
            get
            { return _isEdditing; }
            set
            { SetProperty(ref _isEdditing, value); }
        }

    }
    #endregion

    #region "MILESTONE"
    /// <summary>
    /// Class used to represent a MILESTONE
    /// </summary>
    public class Milestone : BindableBase
    {
        /// <summary>
        /// Date on which the MILESTONE was created
        /// </summary>
        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        /// <summary>
        /// USER who create the MILESTONE
        /// </summary>
        private string _crtnUser;
        public string CrtnUser
        {
            get { return _crtnUser; }
            set { SetProperty(ref _crtnUser, value); }
        }

        /// <summary>
        /// Description of the MILESTONE
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// MILESTONE expiry date
        /// </summary>
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { SetProperty(ref _dueDate, value); }
        }

        /// <summary>
        /// MILESTONE identifier
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// MILESTONE number
        /// </summary>
        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        /// <summary>
        /// State of the MILESTONE. Values: Open (0)/ Closed(1)
        /// </summary>
        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        /// <summary>
        /// Title of the MILESTONE
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
    #endregion

    #region "USER"
    /// <summary>
    /// Class used to represent a USER
    /// </summary>
    public class User : BindableBase
    {
        /// <summary>
        /// Date on which the USER was created
        /// </summary>
        private DateTime _crtnDate;
        public DateTime CrtnDate
        {
            get { return _crtnDate; }
            set { SetProperty(ref _crtnDate, value); }
        }

        /// <summary>
        /// USER identifier
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// Gu code of the USER
        /// </summary>
        private string _gu;
        public string Gu
        {
            get { return _gu; }
            set { SetProperty(ref _gu, value); }
        }

        /// <summary>
        /// Name of the USER
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// Surname of the USER
        /// </summary>
        private string _surName;
        public string SurName
        {
            get { return _surName; }
            set { SetProperty(ref _surName, value); }
        } 
    }
    #endregion

    #region "TYPES/ENUMS"
    /// <summary>
    /// Enum used to representen states
    /// </summary>
    public enum State
    {
        IsOpen,
        IsClosed
    }
    #endregion
}
