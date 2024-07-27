using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Staff;
using api.Models;

namespace api.DTO.Department
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public String Name { get; set; }= string.Empty;
        public List<StaffDTO>? Staffs { get; set; }
    }
}