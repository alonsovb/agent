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
    public partial class ActivityPage : System.Web.UI.Page
    {
        /// <summary>
        /// Usuario que se está usando actualmente.
        /// </summary>
        public AUser CurrentUser { get; set; }
        /// <summary>
        /// Proyecto al que pertenece esta actividad.
        /// </summary>
        public AProject OwnerProject { get; set; }
        /// <summary>
        /// Actividad actual.
        /// </summary>
        public AActivity CurrentActivity { get; set; }
        /// <summary>
        /// Actividad padre de la que se deriva la actual.
        /// </summary>
        public AActivity ParentActivity { get; set; }
        /// <summary>
        /// Lista de actividades hijas (derivadas).
        /// </summary>
        public List<AActivity> ChildActivities { get; set; }
        /// <summary>
        /// Imágenes de esta actividad.
        /// </summary>
        public List<AImage> ActivityImages { get; set; }

        private DBHelper db;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
                CurrentUser = (AUser)Session["user"];
            if (!Page.IsPostBack)
            {
                string sid = Request.QueryString["view"];
                int id;
                int.TryParse(sid, out id);

                db = new DBHelper(AgentUtileries.dbcstring);
                Session["db"] = db;
                CurrentActivity = db.GetActivity(id);

                // Si hay una actividad actual, obtener sus otros datos.
                if (CurrentActivity != null)
                {
                    OwnerProject = db.GetProject(CurrentActivity.ProjectID);
                    ParentActivity = db.GetActivity(CurrentActivity.Parent);
                    ChildActivities = db.GetChildActivities(CurrentActivity.ID);
                    ActivityImages = db.GetImages(CurrentActivity.ID);
                    Session["activity"] = CurrentActivity;
                }
            }
            else
            {
                CurrentActivity = (AActivity)Session["activity"];
                db = (DBHelper)Session["db"];
                if (CurrentActivity != null)
                {
                    OwnerProject = db.GetProject(CurrentActivity.ProjectID);
                    ParentActivity = db.GetActivity(CurrentActivity.Parent);
                    ChildActivities = db.GetChildActivities(CurrentActivity.ID);
                    Title = CurrentActivity.Title;
                }
            }
        }

        protected void BUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                byte[] fileData = new byte[FileUpload.PostedFile.InputStream.Length];
                FileUpload.PostedFile.InputStream.Read(fileData, 0, fileData.Length);
                string fileName = FileUpload.FileName;

                db.InsertImage(CurrentActivity.ID, fileData, fileName);
                
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}