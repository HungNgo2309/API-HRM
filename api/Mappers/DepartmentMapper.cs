using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Department;
using api.Models;

namespace api.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto{
                ID=department.ID,
                Name=department.Name,
                Staffs =department.Staffs.Select(s=>s.ToStaffDto()).ToList()
            };
        }
        public static Department FromCreateToDto(this CreateDepartDto department)
        {
            return new Department{
                Name=department.Name,
            };
        }
    }
}