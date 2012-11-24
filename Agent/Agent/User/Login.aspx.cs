using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agent.Model;
using Agent.Objects;

namespace Agent
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
        }

        protected void BLogin_Click(object sender, EventArgs e)
        {
            string email = TBEmail.Text,
                password = TBPassword.Text;
            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            int id = db.Authenticate(email, password);
            if (id > 0)
            {
                AUser user = db.GetUser(id);
                Session["user"] = user;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/User/Login.aspx");
            }
        }
    }
}