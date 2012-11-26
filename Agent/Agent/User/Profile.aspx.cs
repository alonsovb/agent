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
    public partial class ProfilePage : System.Web.UI.Page
    {
        /// <summary>
        /// Usuario que se está usando actualmente.
        /// </summary>
        public AUser CurrentUser { get; set; }
        /// <summary>
        /// Proyectos del usuario actual.
        /// </summary>
        public List<AProject> Projects { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                DBHelper db = new DBHelper(AgentUtileries.dbcstring);
                CurrentUser = (AUser)Session["user"];
                CurrentUser = db.GetUser(CurrentUser.ID);
                Session["user"] = CurrentUser;
                Projects = db.GetProjects(CurrentUser.ID);
                Page.Title = CurrentUser.Name;
            }
        }
    }
}