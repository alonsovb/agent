using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    public class AActivity
    {
        public int ID { get; set; }
        public string Title {get; set;}
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public Nullable<DateTime> Reminder { get; set; }
        public byte Priority { get; set; }
        public bool Completed { get; set; }
        public int ProjectID { get; set; }
        public string ProjectTitle { get; set; }

        public AActivity(int id, string title, DateTime date, DateTime reminder, byte priority, bool completed)
        {
            ID = id;
            Title = title;
            Date = date;
            Reminder = reminder;
            Priority = priority;
            Completed = completed;
        }

    }

}