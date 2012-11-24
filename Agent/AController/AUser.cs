using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    public class AUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public List<AProject> Projects { get; set; }

        public AUser(string email, string password)
        {
            Projects = new List<AProject>();
            Email = email;
            Password = password;
        }
    }
}