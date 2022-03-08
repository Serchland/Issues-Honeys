using IssuesHoneys.Business;
using IssuesHoneys.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace IssuesHoneys.Services
{
    public class IssueService : BindableBase, IIssueService
    {
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

        public class IssuesResponse : BindableBase
        {
            internal static List<Issue> Issues()
            {
                List<Issue> result = new List<Issue>();

                result.Add(new Issue() { Id = 0, Author = "99ID9878", Description = " Permitir generación de elementos iguales 7 dígitos", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 5855459", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 1, Author = "99ID9878", Description = " Se requiere incializar la aplicacción desde distintos orígenes", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 2, Author = "99ID9878", Description = " Permitir idiomas en y es", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9998857", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 3, Author = "99ID9878", Description = " Conectar con aplicaciones de terceros", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 8524587", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 4, Author = "99ID9878", Description = " Permitir incluir ficheros xml", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 998778", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 5, Author = "99ID5664", Description = " Cambiar la firma para recibir una lista de ints en lugar en un int", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 12112254", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 6, Author = "99ID5664", Description = " Creación de nueva aplicación para gestión de elementos", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 12118855", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 7, Author = "99ID5664", Description = " Nueva funcionalidad para permitir agregar cambios", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9658632", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 8, Author = "99ID1122", Description = " Actualización de documentos", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 175365242112254", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 9, Author = "99ID1122", Description = " Permitir extensiones .jpg, .xml y .bmp", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9512546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 10, Author = "99ID1122", Description = " Nueva funcionalidad que permita chatear entre usuarios", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 95689896", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 11, Author = "99ID1122", Description = " Mejorar el sistema de seguridad", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 9371582", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 12, Author = "99ID1122", Description = " Revisión tiempo de llamadas entre entidades", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 998855110", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 13, Author = "99ID95456", Description = " Incidencia recibida de PRO", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 1112546", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 14, Author = "99ID95456", Description = " Generación de elementos en formato binario", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 2254586", Projects = null, State = State.IsOpen });
                result.Add(new Issue() { Id = 15, Author = "99ID95456", Description = " Generación de elementos en formato json", Labels = GetLabel(0), Millestones = new List<int>() { 1, 2, 3 }, Name = "CAPC 6383566", Projects = null, State = State.IsOpen });


                return result;
            }

            internal static List<Label> GetLabel(int labelId)
            {
                List<Label> result = new List<Label>();

                result.Add(Labels().Find(lb => lb.Id == labelId));

                return result;
            }

            internal static List<Label> Labels()
            {
                List<Label> result = new List<Label>();

                result.Add(new Label() {AssociatedIssues = "0;1;12", Description = "Something isn't working", Id = 0, Name = "bug" });
                result.Add(new Label() { AssociatedIssues = "3;8", Description = "New feature or request", Id = 1, Name = "enhancement" });
                result.Add(new Label() { AssociatedIssues = "4", Description = "Transition phase", Id = 2, Name = "transition" });
                result.Add(new Label() { AssociatedIssues = "14;15", Description = "synchronisation phase", Id = 0, Name = "sincro" });

                return result;
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
