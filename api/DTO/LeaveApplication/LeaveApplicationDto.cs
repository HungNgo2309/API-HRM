using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.StaffRelate;
using api.Models;

namespace api.DTO
{
    public class LeaveApplicationDto
    {
        public int ID { get; set; }
        public int? StaffID { get; set; }
        public String Content { get; set; }=string.Empty;
        public bool Above { get; set; }
        public int? ReasonCategoryID { get; set; } 
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public String Location { get; set; }=string.Empty;
        public bool State { get; set; }
        public String Reject_Reason { get; set; }= string.Empty;
        public DateTime CreateDay { get; set; }
        public int? AccepterID {get;set;}
        public int? SuperiorID  {get;set;}
        public int? AuthorizerID {get;set;}
        public List<StaffRelateDto> StaffRelates {get;set;}= new List<StaffRelateDto>();
    }
}