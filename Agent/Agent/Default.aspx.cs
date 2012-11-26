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
    public partial class DefaultPage : System.Web.UI.Page
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
        /// Recordatorios para el día de hoy.
        /// </summary>
        public List<AActivity> Reminders { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Comprobar que hay una sesión de usuario activa.
            if (Session["user"] != null)
            {
                CurrentUser = (AUser)Session["user"];
                DBHelper db = new DBHelper(AgentUtileries.dbcstring);
                Projects = db.GetProjects(CurrentUser.ID);
                
                Reminders = new List<AActivity>();
                List<AActivity> all = db.GetAllActivities(CurrentUser.ID);

                DateTime today = DateTime.Today,
                    tomorrow = today.AddDays(1);
                foreach (AActivity activity in all)
                {
                    if (activity.Reminder.Value.Ticks > today.Ticks && activity.Reminder.Value.Ticks < tomorrow.Ticks)
                        Reminders.Add(activity);
                }
            }
        }
    }
}