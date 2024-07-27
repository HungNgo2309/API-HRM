using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.StaffRelate
{
    public class CreateStaffRelateDto
    {
        public int ID_Staff { get; set; }
        public int? LeaveApplicationID { get; set; }
    }
}