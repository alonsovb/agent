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
    public partial class WebForm3 : System.Web.UI.Page
    {
        public AUser user;
        public AProject _project;
        private DBHelper m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }
            else
            {
                user = (AUser)Session["user"];
            }
            if (!Page.IsPostBack)
            {
                string sid = Request.QueryString["id"];
                int id;
                int.TryParse(sid, out id);

                m = new Model.DBHelper(AgentUtileries.dbcstring);
                Session["db"] = m;
                _project = m.GetProject(id);
            }
            else
            {
                _project = (AProject)Session["project"];
                m = (DBHelper)Session["db"];
            }
        }
    }
}