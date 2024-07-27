using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ReasonCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }=string.Empty;
        List<LeaveApplication>leaveApplications= new List<LeaveApplication>();

    }
}