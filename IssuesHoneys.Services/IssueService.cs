using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IssuesHoneys.Services
{
    public class IssueService : BindableBase, IIssueService
    {
        private static List<Label> _labels;
        private static List<Millestone> _millestones;

        public IssueService()
        {
            _labels = new List<Label>();
            _millestones = new List<Millestone>();
            Init();
        }

        private void Init()
        {
            _labels.Add(new Label() { AssociatedIssues = "0;1;12", Brush = Brushes.Red, Description = "Something isn't working", Id = 0, Name = "bug" });
            _labels.Add(new Label() { AssociatedIssues = "3;8", Brush = Brushes.LightBlue, Description = "New feature or request", Id = 1, Name = "enhancement" });
            _labels.Add(new Label() { AssociatedIssues = "4", Brush = Brushes.Green, Description = "Transition phase", Id = 2, Name = "transition" });
            _labels.Add(new Label() { AssociatedIssues = "14;15", Brush = Brushes.Purple, Description = "synchronisation phase", Id = 3, Name = "sincro" });
            _labels.Add(new Label() { AssociatedIssues = "14;15", Brush = Brushes.Gold, Description = "associated to version", Id = 4, Name = "version" });

            _millestones.Add(new Millestone() { Description = "PRO V1.12", DueDate = new DateTime(2022, 6, 11), Id = 0, Name = "PRO" });
            _millestones.Add(new Millestone() { Description = "Send PRE", DueDate = new DateTime(2022, 5, 20), Id = 1, Name = "PRE" });
            _millestones.Add(new Millestone() { Description = "Send FOR", DueDate = new DateTime(2022, 4, 20), Id = 2, Name = "FOR" });

        }

        public void CreateLabel(Label newLabel)
        {
            _labels.Add(newLabel);
        }

        public List<Issue> GetIssues()
        {
            return IssuesResponse.Issues();
        }

        public List<Label> GetLabels()
        {
            return IssuesResponse.Labels();
        }

        public List<Millestone> GetMillestones()
        {
            return IssuesResponse.Millestones();
        }

        public void CreateLabel()
        {
            throw new NotImplementedException();
        }

        public class IssuesResponse : BindableBase
        {
            internal static List<Issue> Issues()
            {
                List<Issue> result = new List<Issue>();

                result.Add(new Issue() { Id = 0, Author = "99ID9878", Description = " Permitir generación de elementos iguales 7 dígitos", CrtnDate = new DateTime(2022, 1, 11), Labels = GetLabels(new List<int>() { 0, 1, 2, 3, 4 }), Millestones = GetMillestones(new List<int>() { 1, 2}), Name = "CAPC 5855459", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 1, Author = "99ID9878", Description = " Se requiere incializar la aplicacción desde distintos orígenes", CrtnDate = new DateTime(2022, 1, 15), Labels = null, Millestones = null, Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 2, Author = "99ID9878", Description = " Permitir idiomas en y es", CrtnDate = new DateTime(2022, 2, 01), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 3, Author = "99ID9878", Description = " Conectar con aplicaciones de terceros", CrtnDate = new DateTime(2022, 2, 07), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 8524587", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 4, Author = "99ID9878", Description = " Permitir incluir ficheros xml", CrtnDate = new DateTime(2022, 2, 09), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 998778", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 5, Author = "99ID5664", Description = " Cambiar la firma para recibir una lista de ints en lugar en un int", CrtnDate = new DateTime(2022, 2, 13), Labels = GetLabels(new List<int>() { 0 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 12112254", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 6, Author = "99ID5664", Description = " Creación de nueva aplicación para gestión de elementos", CrtnDate = new DateTime(2022, 2, 15), Labels = GetLabels(new List<int>() { 0 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 12118855", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 7, Author = "99ID5664", Description = " Nueva funcionalidad para permitir agregar cambios", CrtnDate = new DateTime(2022, 2, 19), Labels = GetLabels(new List<int>() { 2, 4 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 9658632", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 8, Author = "99ID1122", Description = " Actualización de documentos", CrtnDate = new DateTime(2022, 2, 23), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 175365242112254", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 9, Author = "99ID1122", Description = " Permitir extensiones .jpg, .xml y .bmp", CrtnDate = new DateTime(2022, 2, 24), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 9512546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 10, Author = "99ID1122", Description = " Nueva funcionalidad que permita chatear entre usuarios", CrtnDate = new DateTime(2022, 2, 24), Labels = GetLabels(new List<int>() { 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 95689896", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 11, Author = "99ID1122", Description = " Mejorar el sistema de seguridad", CrtnDate = new DateTime(2022, 1, 27), Labels = GetLabels(new List<int>() { 0, 1, 4 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 9371582", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 12, Author = "99ID1122", Description = " Revisión tiempo de llamadas entre entidades", CrtnDate = new DateTime(2022, 2, 28), Labels = GetLabels(new List<int>() { 0, 2, 4 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 998855110", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 13, Author = "99ID95456", Description = " Incidencia recibida de PRO", CrtnDate = new DateTime(2022, 2, 8), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 1112546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 14, Author = "99ID95456", Description = " Generación de elementos en formato binario", CrtnDate = new DateTime(2022, 3, 1), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 2254586", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 15, Author = "99ID95456", Description = " Generación de elementos en formato json", CrtnDate = new DateTime(2022, 3, 4), Labels = GetLabels(new List<int>() { 0, 1 }), Millestones = GetMillestones(new List<int>() { 1 }), Name = "CAPC 6383566", Projects = null, State = State.IsOpen });


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

            internal static List<Millestone> GetMillestones(List<int> millestonesId)
            {
                List<Millestone> result = Millestones();

                result = (from ml in _millestones
                          where millestonesId.Any(it => it == ml.Id)
                          select ml).ToList();

                return result;
            }

            internal static List<Millestone> Millestones()
            {
                return _millestones;
            }
        }
    }
}
