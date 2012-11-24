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
    public partial class WebForm2 : System.Web.UI.Page
    {
        public AUser user;
        public AActivity activity;
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

                m = new DBHelper(AgentUtileries.dbcstring);
                Session["db"] = m;
                activity = m.GetActivity(id);
            }
            else
            {
                activity = (AActivity)Session["activity"];
                m = (DBHelper)Session["db"];
            }
        }
    }
}