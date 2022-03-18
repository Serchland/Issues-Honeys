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

        public IssueService()
        {
            _labels = new List<Label>();
            Init();
        }

        private void Init()
        {
            _labels.Add(new Label() { AssociatedIssues = "0;1;12", Brush = Brushes.Red, Description = "Something isn't working", Id = 0, Name = "bug" });
            _labels.Add(new Label() { AssociatedIssues = "3;8", Brush = Brushes.LightBlue, Description = "New feature or request", Id = 1, Name = "enhancement" });
            _labels.Add(new Label() { AssociatedIssues = "4", Brush = Brushes.Green, Description = "Transition phase", Id = 2, Name = "transition" });
            _labels.Add(new Label() { AssociatedIssues = "14;15", Brush = Brushes.Purple, Description = "synchronisation phase", Id = 3, Name = "sincro" });
            _labels.Add(new Label() { AssociatedIssues = "14;15", Brush = Brushes.Gold, Description = "associated to version", Id = 4, Name = "version" });
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

                result.Add(new Issue() { Id = 0, Author = "99ID9878", Description = " Permitir generación de elementos iguales 7 dígitos", Labels = GetLabels(new List<int>(){ 0, 1, 2, 3, 4}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 5855459", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 1, Author = "99ID9878", Description = " Se requiere incializar la aplicacción desde distintos orígenes", Labels = GetLabels(new List<int>(){ 1}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 2, Author = "99ID9878", Description = " Permitir idiomas en y es", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 2, 3, 4 }, Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 3, Author = "99ID9878", Description = " Conectar con aplicaciones de terceros", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() {3 }, Name = "CAPC 8524587", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 4, Author = "99ID9878", Description = " Permitir incluir ficheros xml", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 1, 3 }, Name = "CAPC 998778", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 5, Author = "99ID5664", Description = " Cambiar la firma para recibir una lista de ints en lugar en un int", Labels = GetLabels(new List<int>(){ 0}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 12112254", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 6, Author = "99ID5664", Description = " Creación de nueva aplicación para gestión de elementos", Labels = GetLabels(new List<int>(){ 0}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 12118855", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 7, Author = "99ID5664", Description = " Nueva funcionalidad para permitir agregar cambios", Labels = GetLabels(new List<int>(){ 2, 4}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9658632", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 8, Author = "99ID1122", Description = " Actualización de documentos", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 3, 4 }, Name = "CAPC 175365242112254", Projects = null, State = State.IsClosed});
                result.Add(new Issue() { Id = 9, Author = "99ID1122", Description = " Permitir extensiones .jpg, .xml y .bmp", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 1, 4 }, Name = "CAPC 9512546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 10, Author = "99ID1122", Description = " Nueva funcionalidad que permita chatear entre usuarios", Labels = GetLabels(new List<int>(){ 1}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 95689896", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 11, Author = "99ID1122", Description = " Mejorar el sistema de seguridad", Labels = GetLabels(new List<int>(){ 0, 1, 4}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9371582", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 12, Author = "99ID1122", Description = " Revisión tiempo de llamadas entre entidades", Labels = GetLabels(new List<int>(){ 0, 2, 4}), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 998855110", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 13, Author = "99ID95456", Description = " Incidencia recibida de PRO", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 1, 3 }, Name = "CAPC 1112546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 14, Author = "99ID95456", Description = " Generación de elementos en formato binario", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 0, 2, 3 }, Name = "CAPC 2254586", Projects = null, State = State.IsClosed });
                result.Add(new Issue() { Id = 15, Author = "99ID95456", Description = " Generación de elementos en formato json", Labels = GetLabels(new List<int>(){ 0, 1}), Millestones = new List<int>() { 0, 1, 4 }, Name = "CAPC 6383566", Projects = null, State = State.IsOpen });


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

            internal static List<Millestone> Millestones()
            {
                List<Millestone> result = new List<Millestone>();

                result.Add(new Millestone() { Description = "Funcionalidad versión PRODUCCIÓN", DueDate = new DateTime(2022,6,11), Id = 0, Name = "PRO" });
                result.Add(new Millestone() { Description = "Funcionalidad versión PRE EXPLOTACÍÓN", DueDate = new DateTime(2022, 5, 20), Id = 1, Name = "PRE" });
                result.Add(new Millestone() { Description = "Funcionalidad versión FORMACIÓN", DueDate = new DateTime(2022, 4, 20), Id = 2, Name = "FOR" });

                return result;
            }
        }
    }
}
