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
    public partial class ByDatePage : System.Web.UI.Page
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
        /// Lista de actividades para esta fecha.
        /// </summary>
        public List<AActivity> ActivityList { get; set; }
        /// <summary>
        /// Texto que representa el lapso seleccionado.
        /// </summary>
        public string TimeText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                CurrentUser = (AUser)Session["user"];
                DBHelper db = new DBHelper(AgentUtileries.dbcstring);

                DateTime today = DateTime.Today,
                    end;
                string sdate = Request.QueryString["for"];
                switch (sdate)
                {
                    case "today":
                        end = today.AddDays(1);
                        TimeText = "hoy";
                        break;
                    case "week":
                        end = today.AddDays(7);
                        TimeText = "esta semana";
                        break;
                    case "month":
                        end = today.AddMonths(1);
                        TimeText = "este mes";
                        break;
                    case "year":
                        end = today.AddYears(1);
                        TimeText = "este año";
                        break;
                    default:
                        end = today.AddDays(7);
                        TimeText = "esta semana";
                        break;
                }

                List<AActivity> all = db.GetAllActivities(CurrentUser.ID);
                Projects = db.GetProjects(CurrentUser.ID);
                ActivityList = new List<AActivity>();
                foreach (AActivity activity in all)
                {
                    if (activity.Date.Value.Ticks > today.Ticks && activity.Date.Value.Ticks < end.Ticks)
                        ActivityList.Add(activity);
                }
                Title = "Actividades para " + TimeText;
            }
        }
    }
}