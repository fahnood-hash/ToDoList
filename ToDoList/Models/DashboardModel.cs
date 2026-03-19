using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class DashboardModel
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int InProgressTasks { get; set; }
        public double CompletedPercentage { get; set; }
        //public Dictionary<string, int> CategoryCounts { get; set; }
    }
}