using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Models;

namespace api.DTOs.Staff
{
    public class StaffDTO
    {
        public int ID { get; set; }
        public String Name { get; set; }=string.Empty;
        public String Email { get; set; }=string.Empty;
        public int Phone { get; set; }
        public String Password {get;set;}=string.Empty;
        public String TelegramID{get;set;}=string.Empty;
        public int? DepartmentID { get; set; }
        public int? PositionID {get;set;}
        public List<LeaveApplicationDto>? LeaveApplications { get; set; }
    }
}