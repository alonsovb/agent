using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using Agent.Model;
using Agent.Objects;

namespace Agent
{

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AgenteService
    {
        public string dbcstring = System.Configuration.ConfigurationManager.ConnectionStrings["SQLAgente"].ConnectionString;

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        #region Usuarios
        
        /// <summary>
        /// Registra un usuario en el sistema.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <param name="name">Nombre del usuario.</param>
        /// <returns>Datos del usuario registrado.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement RegisterUser(string email, string password, string name)
        {
            XElement x = new XElement("users");

            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            db.RegisterUser(email, password, name);

            return x;
        }

        /// <summary>
        /// Autentifica los credenciales de un usuario.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Datos del usuario si son correctos.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement Authenticate(string email, string password)
        {
            XElement x = new XElement("users");

            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            int result = db.Authenticate(email, password);
            if (result > 0)
            {
                AUser user = db.GetUser(result);
                XElement xuser = new XElement("user");
                xuser.Add(new XElement("ID", user.ID));
                xuser.Add(new XElement("Email", user.Email));
                xuser.Add(new XElement("Name", user.Name));
                x.Add(xuser);
            }

            return x;
        }

        /// <summary>
        /// Obtiene un usuario según su Identificación.
        /// </summary>
        /// <param name="userid">Identificación del usuario.</param>
        /// <returns>Datos del usuario.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetUser(int userid)
        {
            XElement x = new XElement("users");

            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            AUser user = db.GetUser(userid);
            if (user != null)
            {
                XElement xuser = new XElement("user");
                xuser.Add(new XElement("ID", user.ID));
                xuser.Add(new XElement("Email", user.Email));
                xuser.Add(new XElement("Name", user.Name));
                x.Add(xuser);
            }

            return x;
        }

        /// <summary>
        /// Actualiza el nombre de un usuario.
        /// </summary>
        /// <param name="userid">Identificador del usuario.</param>
        /// <param name="name">Nuev nombre del usuario.</param>
        /// <returns>Datos del usuario.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement UpdateUser(int userid, string name)
        {
            XElement x = new XElement("users");

            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            db.UpdateName(userid, name);
            
            AUser user = db.GetUser(userid);
            if (user != null)
            {
                XElement xuser = new XElement("user");
                xuser.Add(new XElement("ID", user.ID));
                xuser.Add(new XElement("Email", user.Email));
                xuser.Add(new XElement("Name", user.Name));
                x.Add(xuser);
            }

            return x;
        }
        #endregion

        #region Proyectos

        /// <summary>
        /// Obtiene los proyectos de un usuario.
        /// </summary>
        /// <param name="user">Identificación del usuario.</param>
        /// <returns>Lista de proyectos.</returns>
        [OperationContract]
        [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        public XElement GetProjects(int user)
        {
            XElement x = new XElement("projects");

            DBHelper db = new Model.DBHelper(dbcstring);
            List<AProject> projects = db.GetProjects(user);
            foreach (AProject project in projects)
            {
                XElement xproject = new XElement("project");
                xproject.Add(new XElement("id", project.ID.ToString()));
                xproject.Add(new XElement("title", project.Title.Trim()));
                int actCount = db.GetActivities(project.ID).Count;
                xproject.Add(new XElement("activitycount", actCount.ToString()));
                x.Add(xproject);
            }
            return x;
        }

        /// <summary>
        /// Obtiene los datos de un proyecto.
        /// </summary>
        /// <param name="projectid">Identificador del proyecto.</param>
        /// <returns>Datos del proyecto.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetProject(int projectid)
        {
            XElement x = new XElement("projects");

            DBHelper db = new Model.DBHelper(dbcstring);
            AProject project = db.GetProject(projectid);
            if (project != null)
            {
                XElement xproject = new XElement("project");
                xproject.Add(new XElement("id", project.ID.ToString()));
                xproject.Add(new XElement("title", project.Title.Trim()));
                int actCount = db.GetActivities(project.ID).Count;
                xproject.Add(new XElement("activitycount", actCount.ToString()));
                x.Add(xproject);
            }
            return x;
        }

        /// <summary>
        /// Registra un nuevo proyecto en el sistema.
        /// </summary>
        /// <param name="user">Usuario al que pertenece.</param>
        /// <param name="title">Título del proyecto.</param>
        /// <returns>Datos del proyecto registrado.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement RegisterProject(int user, string title)
        {
            XElement x = new XElement("projects");

            DBHelper db = new DBHelper(dbcstring);
            
            int registeredId = db.RegisterProject(user, title);
            AProject project = db.GetProject(registeredId);
            
            if (project != null)
            {
                XElement xproject = new XElement("project");
                xproject.Add(new XElement("id", project.ID.ToString()));
                xproject.Add(new XElement("title", project.Title.Trim()));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Actualiza el título de un proyecto.
        /// </summary>
        /// <param name="projectid">Identificador del proyecto.</param>
        /// <param name="title">Nuevo título del proyecto.</param>
        /// <returns>Datos del proyecto.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement UpdateProjectTitle(int projectid, string title)
        {
            XElement x = new XElement("projects");

            DBHelper db = new DBHelper(dbcstring);
            db.UpdateProjectTitle(projectid, title);

            AProject project = db.GetProject(projectid);
            if (project != null)
            {
                XElement xproject = new XElement("project");
                xproject.Add(new XElement("id", project.ID.ToString()));
                xproject.Add(new XElement("title", project.Title.Trim()));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Elimina un proyecto según su Identificador.
        /// </summary>
        /// <param name="projectid">Identificador del proyecto.</param>
        /// <returns>XML vacío.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement DeleteProject(int projectid)
        {
            XElement x = new XElement("projects");

            DBHelper db = new DBHelper(dbcstring);

            db.DeleteProject(projectid);

            return x;
        }

        #endregion

        #region Actividades

        /// <summary>
        /// Obtiene las actividades de un proyecto.
        /// </summary>
        /// <param name="project">Identificador del proyecto.</param>
        /// <returns>Lista de actividades.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetActivities(int project)
        {
            XElement x = new XElement("activities");

            DBHelper db = new Model.DBHelper(dbcstring);
            List<AActivity> activities = db.GetActivities(project);
            foreach (AActivity activity in activities)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", activity.ID.ToString()));
                xproject.Add(new XElement("title", activity.Title.Trim()));
                xproject.Add(new XElement("date", activity.Date));
                xproject.Add(new XElement("created", activity.Created));
                xproject.Add(new XElement("reminder", activity.Reminder));
                xproject.Add(new XElement("priority", activity.Priority));
                xproject.Add(new XElement("completed", activity.Completed));
                xproject.Add(new XElement("parent", activity.Parent));
                x.Add(xproject);
            }
            return x;
        }

        /// <summary>
        /// Obtiene las actividades de un usuario.
        /// </summary>
        /// <param name="user">Identificador del usuario.</param>
        /// <returns>Lista de actividades.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetAllActivities(int user)
        {
            XElement x = new XElement("activities");

            DBHelper db = new Model.DBHelper(dbcstring);
            List<AActivity> activities = db.GetAllActivities(user);
            foreach (AActivity activity in activities)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", activity.ID.ToString()));
                xproject.Add(new XElement("title", activity.Title.Trim()));
                xproject.Add(new XElement("date", activity.Date));
                xproject.Add(new XElement("created", activity.Created));
                xproject.Add(new XElement("reminder", activity.Reminder));
                xproject.Add(new XElement("priority", activity.Priority));
                xproject.Add(new XElement("completed", activity.Completed));
                xproject.Add(new XElement("parent", activity.Parent));
                xproject.Add(new XElement("projectid", activity.ProjectID));
                xproject.Add(new XElement("projecttitle", activity.ProjectTitle));
                x.Add(xproject);
            }
            return x;
        }

        /// <summary>
        /// Obtiene los datos de una actividad.
        /// </summary>
        /// <param name="activity">Identificador de la actividad.</param>
        /// <returns>Datos de la actividad.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetActivity(int activity)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);
            AActivity aactivity = db.GetActivity(activity);

            if (aactivity != null)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", aactivity.ID.ToString()));
                xproject.Add(new XElement("title", aactivity.Title.Trim()));
                xproject.Add(new XElement("date", aactivity.Date));
                xproject.Add(new XElement("created", aactivity.Created));
                xproject.Add(new XElement("reminder", aactivity.Reminder));
                xproject.Add(new XElement("priority", aactivity.Priority));
                xproject.Add(new XElement("completed", aactivity.Completed));
                xproject.Add(new XElement("parent", aactivity.Parent));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Obtiene las actividades derivadas de otra actividad.
        /// </summary>
        /// <param name="activity">Identificador de la actividad padre.</param>
        /// <returns>Lista de actividades</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement GetChildActivities(int activity)
        {
            XElement x = new XElement("activities");

            DBHelper db = new Model.DBHelper(dbcstring);
            List<AActivity> activities = db.GetChildActivities(activity);
            foreach (AActivity aactivity in activities)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", aactivity.ID.ToString()));
                xproject.Add(new XElement("title", aactivity.Title.Trim()));
                xproject.Add(new XElement("date", aactivity.Date));
                xproject.Add(new XElement("created", aactivity.Created));
                xproject.Add(new XElement("reminder", aactivity.Reminder));
                xproject.Add(new XElement("priority", aactivity.Priority));
                xproject.Add(new XElement("completed", aactivity.Completed));
                xproject.Add(new XElement("parent", aactivity.Parent));
                x.Add(xproject);
            }
            return x;
        }

        /// <summary>
        /// Registra una nueva actividad en el sistema.
        /// </summary>
        /// <param name="project">Proyecto al que pertenece.</param>
        /// <param name="title">Título de la actividad.</param>
        /// <param name="parentActivity">Actividad padre de la nueva actividad.</param>
        /// <param name="priority">Prioridad de la actividad.</param>
        /// <param name="due">Fecha en que ocurre la actividad.</param>
        /// <param name="reminder">Fecha de recordatorio de la actividad.</param>
        /// <param name="completed">Completitud de la actividad.</param>
        /// <returns>Datos de la actividad.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement RegisterActivity(int project, string title, int parentActivity, int priority,
            string due, string reminder, int completed)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);

            DateTime ddue = DateTime.Parse(due),
                dreminder = DateTime.Parse(reminder);
            bool bcompleted = (completed > 0);

            int registeredid = db.RegisterActivity(project, title, ddue, parentActivity,
                Convert.ToByte(priority), dreminder, bcompleted);
            AActivity activity = db.GetActivity(registeredid);
            
            if (activity != null)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", activity.ID.ToString()));
                xproject.Add(new XElement("title", activity.Title.Trim()));
                xproject.Add(new XElement("date", activity.Date));
                xproject.Add(new XElement("created", activity.Created));
                xproject.Add(new XElement("reminder", activity.Reminder));
                xproject.Add(new XElement("priority", activity.Priority));
                xproject.Add(new XElement("completed", activity.Completed));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Actualiza la completitud de una actividad.
        /// </summary>
        /// <param name="activity">Identificador de la actividad.</param>
        /// <param name="completed">Completitud de la actividad.</param>
        /// <returns>Datos de la actividad.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement UpdateCompleted(int activity, int completed)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);

            bool bcompleted = (completed > 0);
            db.UpdateCompleted(activity, bcompleted);

            AActivity aactivity = db.GetActivity(activity);

            if (aactivity != null)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", aactivity.ID.ToString()));
                xproject.Add(new XElement("title", aactivity.Title.Trim()));
                xproject.Add(new XElement("date", aactivity.Date));
                xproject.Add(new XElement("created", aactivity.Created));
                xproject.Add(new XElement("reminder", aactivity.Reminder));
                xproject.Add(new XElement("priority", aactivity.Priority));
                xproject.Add(new XElement("completed", aactivity.Completed));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Actualizar prioridad de una actividad.
        /// </summary>
        /// <param name="activity">Identificador de la actividad.</param>
        /// <param name="priority">Prioridad de la actividad.</param>
        /// <returns>Datos de la actividad.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement UpdatePriority(int activity, int priority)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);

            db.UpdatePriority(activity, Convert.ToByte(priority));

            AActivity aactivity = db.GetActivity(activity);

            if (aactivity != null)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", aactivity.ID.ToString()));
                xproject.Add(new XElement("title", aactivity.Title.Trim()));
                xproject.Add(new XElement("date", aactivity.Date));
                xproject.Add(new XElement("created", aactivity.Created));
                xproject.Add(new XElement("reminder", aactivity.Reminder));
                xproject.Add(new XElement("priority", aactivity.Priority));
                xproject.Add(new XElement("completed", aactivity.Completed));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Actualiza el recordatorio de una actividad.
        /// </summary>
        /// <param name="activity">Identificador de la actividad.</param>
        /// <param name="reminder">Nuevo recordatorio de la actividad.</param>
        /// <returns>Datos de la actividad.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement UpdateReminder(int activity, string reminder)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);

            db.UpdateReminder(activity, DateTime.Parse(reminder));

            AActivity aactivity = db.GetActivity(activity);

            if (aactivity != null)
            {
                XElement xproject = new XElement("activity");
                xproject.Add(new XElement("id", aactivity.ID.ToString()));
                xproject.Add(new XElement("title", aactivity.Title.Trim()));
                xproject.Add(new XElement("date", aactivity.Date));
                xproject.Add(new XElement("created", aactivity.Created));
                xproject.Add(new XElement("reminder", aactivity.Reminder));
                xproject.Add(new XElement("priority", aactivity.Priority));
                xproject.Add(new XElement("completed", aactivity.Completed));
                x.Add(xproject);
            }

            return x;
        }

        /// <summary>
        /// Elimina una actividad según su Identificador.
        /// </summary>
        /// <param name="activityid">Identificador de la actividad.</param>
        /// <returns>XML vacío.</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml)]
        public XElement DeleteActivity(int activityid)
        {
            XElement x = new XElement("activities");

            DBHelper db = new DBHelper(dbcstring);

            db.DeleteActivity(activityid);

            return x;
        }

        #endregion
    }
}
