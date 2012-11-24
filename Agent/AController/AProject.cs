using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    public class AProject
    {
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public int ID { get; set; }
        public List<AActivity> Activities { get; set; }

        public AProject(int id, string title)
        {
            Activities = new List<AActivity>();
            ID = id;
            Title = title;
        }

        public double CompleteRatio()
        {
            int completeActivities = 0;
            foreach (AActivity a in Activities)
            {
                if (a.Completed) completeActivities++;
            }
            return Activities.Count > 0 ? completeActivities / Activities.Count : 0;
        }

        public void AddActivity(AActivity newActivity)
        {
            Activities.Add(newActivity);
        }
    }
}