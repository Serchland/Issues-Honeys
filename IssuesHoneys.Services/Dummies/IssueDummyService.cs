using IssuesHoneys.Business.Types;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IssuesHoneys.Services.Dummies
{
    /// <summary>
    /// Dummy functionality should only be used for development or demonstration purposes.
    /// </summary>
    public class IssueDummyService : BindableBase, IIssueService
    {
        private static List<Label> _labels;
        private static List<Milestone> _millestones;

        public IssueDummyService()
        {
            _labels = new List<Label>();
            _millestones = new List<Milestone>();
            Init();
        }

        private void Init()
        {
            _labels.Add(new Label() { Color = Brushes.Red, Description = "Something isn't working", Id = 0, Name = "bug", TotalIssues=5 });
            _labels.Add(new Label() { Color = Brushes.LightBlue, Description = "New feature or request", Id = 1, Name = "enhancement", TotalIssues = 5 });
            _labels.Add(new Label() { Color = Brushes.Green, Description = "Transition phase", Id = 2, Name = "transition", TotalIssues = 5 });
            _labels.Add(new Label() { Color = Brushes.Purple, Description = "synchronisation phase", Id = 3, Name = "sincro", TotalIssues = 5 });
            _labels.Add(new Label() { Color = Brushes.Gold, Description = "associated to version", Id = 4, Name = "version", TotalIssues = 5 });

            _millestones.Add(new Milestone() { Description = "PRO V1.12", DueDate = new DateTime(2022, 6, 11), Id = 0, Title = "PRO" });
            _millestones.Add(new Milestone() { Description = "Send PRE", DueDate = new DateTime(2022, 5, 20), Id = 1, Title = "PRE" });
            _millestones.Add(new Milestone() { Description = "Send FOR", DueDate = new DateTime(2022, 4, 20), Id = 2, Title = "FOR" });
        }

        /// <summary>
        /// Create a LABEL in the sql model. Implementation of IIssueService.CreateLabel
        /// </summary>
        /// <param name="newLabel"></param>
        public void CreateLabel(Label newLabel)
        {
            _labels.Add(newLabel);
        }

        /// <summary>
        /// Obtain ISSUES from the sql model. Implementation of IIssueService.GetIssues
        /// </summary>
        public List<Issue> GetIssues()
        {
            return IssuesResponse.Issues();
        }

        /// <summary>
        /// Obtain LABELS from the SQL model. Implementation of IIssueService.GetLabels
        /// </summary>
        /// <returns>List<Label></returns>
        public List<Label> GetLabels(LabelType labelType)
        {
            return IssuesResponse.Labels();
        }

        /// <summary>
        /// Obtain MILESTONES from the SQL model. Implementation of IIssueService.GetMillestones
        /// </summary>
        /// <returns>List<Milestone></returns>
        public List<Milestone> GetMilestones()
        {
            return IssuesResponse.Millestones();
        }

        /// <summary>
        /// Obtain USERS from the SQL model. Implementation of IIssueService.GetUsers
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetUsers()
        {
            return IssuesResponse.Users();
        }

        public int GetIssuesWithLabelId(int labelID)
        {
            return 10;
        }

        public List<User> GetAssignedUsersToIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public List<Milestone> GetAssignedMilestonesToIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public List<Label> GetAssignedLabelsToIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLabel(Label label)
        {
            throw new NotImplementedException();
        }

        public void DeleteLabel(int labelId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int? userId)
        {
            throw new NotImplementedException();
        }

        public Issue GetIssueById(int issueId)
        {
            throw new NotImplementedException();
        }

        public void AddUserToIssue(int detailId, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserToIssue(int detailId, int userId)
        {
            throw new NotImplementedException();
        }

        public void AddLabelToIssue(int issueId, int labelId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLabelToIssue(int detailId, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMilestoneToIssue(int issueId, int milestoneId)
        {
            throw new NotImplementedException();
        }

        public void AddMilestoneToIssue(int issueId, int milestoneId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dummies responses uses by IssueDummyService
        /// </summary>
        internal class IssuesResponse : BindableBase
        {
            internal static List<Issue> Issues()
            {
                List<Issue> result = new List<Issue>();

                //result.Add(new Issue() { Id = 0, CrtnUser = "99ID9878", Body = " Permitir generación de elementos iguales 7 dígitos", CrtnDate = new DateTime(2022, 1, 11), Labels = GetLabels(new List<int>() { 0, 1, 2, 3, 4 }), Milestones = GetMillestones(new List<int>() { 1, 2}), Title = "CAPC 5855459", Projects = null, State = State.IsClosed, TotalComments=5 });
                //result.Add(new Issue() { Id = 1, CrtnUser = "99ID9878", Body = " Se requiere incializar la aplicacción desde distintos orígenes", CrtnDate = new DateTime(2022, 1, 15), Labels = null, Milestones = null, Title = "CAPC 9998857", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 2, CrtnUser = "99ID9878", Body = " Permitir idiomas en y es", CrtnDate = new DateTime(2022, 2, 01), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 9998857", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 3, CrtnUser = "99ID9878", Body = " Conectar con aplicaciones de terceros", CrtnDate = new DateTime(2022, 2, 07), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 8524587", Projects = null, State = State.IsClosed });
                //result.Add(new Issue() { Id = 4, CrtnUser = "99ID9878", Body = " Permitir incluir ficheros xml", CrtnDate = new DateTime(2022, 2, 09), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 3 }), Title = "CAPC 998778", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 5, CrtnUser = "99ID5664", Body = " Cambiar la firma para recibir una lista de ints en lugar en un int", CrtnDate = new DateTime(2022, 2, 13), Labels = GetLabels(new List<int>() { 0 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 12112254", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 6, CrtnUser = "99ID5664", Body = " Creación de nueva aplicación para gestión de elementos", CrtnDate = new DateTime(2022, 2, 15), Labels = GetLabels(new List<int>() { 0 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 12118855", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 7, CrtnUser = "99ID5664", Body = " Nueva funcionalidad para permitir agregar cambios", CrtnDate = new DateTime(2022, 2, 19), Labels = GetLabels(new List<int>() { 2, 4 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 9658632", Projects = null, State = State.IsClosed });
                //result.Add(new Issue() { Id = 8, CrtnUser = "99ID1122", Body = " Actualización de documentos", CrtnDate = new DateTime(2022, 2, 23), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1, 2 }), Title = "CAPC 175365242112254", Projects = null, State = State.IsClosed });
                //result.Add(new Issue() { Id = 9, CrtnUser = "99ID1122", Body = " Permitir extensiones .jpg, .xml y .bmp", CrtnDate = new DateTime(2022, 2, 24), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 9512546", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 10, CrtnUser = "99ID1122", Body = " Nueva funcionalidad que permita chatear entre usuarios", CrtnDate = new DateTime(2022, 2, 24), Labels = GetLabels(new List<int>() { 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 95689896", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 11, CrtnUser = "99ID1122", Body = " Mejorar el sistema de seguridad", CrtnDate = new DateTime(2022, 1, 27), Labels = GetLabels(new List<int>() { 0, 1, 4 }), Milestones = GetMillestones(new List<int>() { 2, 1 }), Title = "CAPC 9371582", Projects = null, State = State.IsClosed });
                //result.Add(new Issue() { Id = 12, CrtnUser = "99ID1122", Body = " Revisión tiempo de llamadas entre entidades", CrtnDate = new DateTime(2022, 2, 28), Labels = GetLabels(new List<int>() { 0, 2, 4 }), Milestones = null, Title = "CAPC 998855110", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 13, CrtnUser = "99ID95456", Body = " Incidencia recibida de PRO", CrtnDate = new DateTime(2022, 2, 8), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 1112546", Projects = null, State = State.IsOpen });
                //result.Add(new Issue() { Id = 14, CrtnUser = "99ID95456", Body = " Generación de elementos en formato binario", CrtnDate = new DateTime(2022, 3, 1), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 2, 3 }), Title = "CAPC 2254586", Projects = null, State = State.IsClosed });
                //result.Add(new Issue() { Id = 15, CrtnUser = "99ID95456", Body = " Generación de elementos en formato json", CrtnDate = new DateTime(2022, 3, 4), Labels = GetLabels(new List<int>() { 0, 1 }), Milestones = GetMillestones(new List<int>() { 1 }), Title = "CAPC 6383566", Projects = null, State = State.IsOpen });


                return result;
            }

            internal static List<Label> GetLabels(List<int> labelsId)
            {
                List<Label> result = Labels();

                result = (from lb in _labels
                          where labelsId.Any(it => it == lb.Id)
                          select lb).ToList();

                return result;
            }

            internal static List<Label> Labels()
            {
                return _labels;
            }

            internal static List<Milestone> GetMillestones(List<int> millestonesId)
            {
                List<Milestone> result = Millestones();

                result = (from ml in _millestones
                          where millestonesId.Any(it => it == ml.Id)
                          select ml).ToList();

                return result;
            }

            internal static List<Milestone> Millestones()
            {
                return _millestones;
            }

            internal static List<User> Users()
            {
                List<User> result = new List<User>();

                result.Add(new User { CrtnDate = DateTime.Now, Id = 1, Name = "Justine", SurName = "Rice", Gu = "99GU0001" });
                result.Add(new User { CrtnDate = DateTime.Now, Id = 2, Name = "Julia", SurName = "Ross", Gu = "99GU0002" });
                result.Add(new User { CrtnDate = DateTime.Now, Id = 3, Name = "Nikolett", SurName = "Russell", Gu = "99GU0003" });
                result.Add(new User { CrtnDate = DateTime.Now, Id = 4, Name = "Nicholas", SurName = "Porter", Gu = "99GU0004" });
                result.Add(new User { CrtnDate = DateTime.Now, Id = 5, Name = "Matilde", SurName = "Wood", Gu = "99GU0005" });

                return result;
            }
        }
    }
}
