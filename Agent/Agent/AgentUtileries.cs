using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent
{
    public class AgentUtileries
    {
        public static string dbcstring = System.Configuration.ConfigurationManager.ConnectionStrings["SQLAgente"].ConnectionString;
        public static string[] priorities = { "Indefinida", "Baja", "Regular", "Alta" };
    }
}