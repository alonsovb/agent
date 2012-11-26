using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Agent.Objects;
using Agent.Model;

namespace Agent
{
    public partial class ProjectPage : System.Web.UI.Page
    {
        /// <summary>
        /// Usuario que se está usando actualmente.
        /// </summary>
        public AUser CurrentUser { get; set; }
        /// <summary>
        /// Proyectos del usuario actual.
        /// </summary>
        public List<AProject> Projects { get; set; }
        /// <summary>
        /// Proyecto actual.
        /// </summary>
        public AProject CurrentProject { get; set; }
        /// <summary>
        /// Lista de actividades de este proyecto.
        /// </summary>
        public List<AActivity> ActivityList { get; set; }
        /// <summary>
        /// Cantidad de actividades completadas de este proyecto.
        /// </summary>
        public int Completed { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                CurrentUser = (AUser)Session["user"];
                DBHelper db = new DBHelper(AgentUtileries.dbcstring);

                Projects = db.GetProjects(CurrentUser.ID);

                string sid = Request.QueryString["view"];
                int id;
                int.TryParse(sid, out id);

                CurrentProject = db.GetProject(id);
                ActivityList = db.GetActivities(id);
                foreach(AActivity a in ActivityList) {
                    if (a.Completed) Completed++;
                }
                Title = CurrentProject.Title;
            }
        }
    }
}