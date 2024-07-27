using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class StaffRelate
    {
        public int ID {get;set;}
        public int ID_Staff { get; set; }
        public int? LeaveApplicationID { get; set; }
        public LeaveApplication? LeaveApplication{get;set;}
    }
}