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
    public partial class WebForm1 : System.Web.UI.Page
    {
        public AUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                //DBHelper db = new DBHelper(AgentUtileries.dbcstring);
                //int id = db.Authenticate("a@b.c", "a");
                //if (id > 0)
                //{
                //    AUser user = db.GetUser(id);
                //    Session["user"] = user;
                //    Response.Redirect("~/Default.aspx");
                //    return;
                //}
                Response.Redirect("~/User/Login.aspx");
            }
            else
            {
                user = (AUser)Session["user"];
                HyperLink profileLink = new HyperLink();
                profileLink.NavigateUrl = "~/User/Profile.aspx?id=" + user.ID;
                profileLink.Text = "<h2 class='profilename'>" + user.Name + "</h2>";
                OptionPanel.Controls.Add(profileLink);
            }
        }
    }
}