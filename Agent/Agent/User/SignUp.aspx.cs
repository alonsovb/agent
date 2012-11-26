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
    public partial class SignUpPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BRegisterUser_Click(object sender, EventArgs e)
        {
            string email = TBEmail.Text,
                password = TBPassword.Text,
                name = TBName.Text;
            DBHelper db = new DBHelper(AgentUtileries.dbcstring);
            db.RegisterUser(email, password, name);
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