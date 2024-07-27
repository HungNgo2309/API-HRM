using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Staff
    {
        public int ID { get; set; }
        public String Name { get; set; }=string.Empty;
        [Required]
        [EmailAddress]
        public String Email { get; set; }=string.Empty;
        public int Phone { get; set; }
        [Required]
        public String Password { get; set; }=string.Empty;
        public String TelegramID {get;set;}= string.Empty;
        public int? PositionID {get;set;}
        public Position? Position {get;set;}
        public int? DepartmentID { get; set; }
        public Department? Department {get;set;}
        public List<LeaveApplication> LeaveApplications { get; set; }=new List<LeaveApplication>();

    }
}