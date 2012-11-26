using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Agent.Objects;
using Agent.Model;

namespace Agent
{
    /// <summary>
    /// Summary description for GetImage
    /// </summary>
    public class GetImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            AImage image = new DBHelper(AgentUtileries.dbcstring).GetImage(int.Parse(id));
            
            char[] sep = { '.' };
            string[] splittedName = image.FileName.Split(sep);
            string type = splittedName[splittedName.Length - 1];
            context.Response.ContentType = "image/" + type;

            context.Response.OutputStream.Write(image.FileData, 0, image.FileData.Length);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}