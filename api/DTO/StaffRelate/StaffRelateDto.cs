using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.StaffRelate
{
    public class StaffRelateDto
    {
        public int ID {get;set;}
        public int ID_Staff { get; set; }
        public int? LeaveApplicationID { get; set; }
    }
}