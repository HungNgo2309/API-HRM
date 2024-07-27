using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.StaffRelate;
using api.Models;

namespace api.Mappers
{
    public static class StaffRelateMapper
    {
        public static StaffRelateDto ToStaffRelateDto (this StaffRelate staffRelate)
        {
            return new StaffRelateDto{
                ID= staffRelate.ID,
                ID_Staff=staffRelate.ID_Staff,
                LeaveApplicationID=staffRelate.LeaveApplicationID
            };
        }
        public static StaffRelate ToCreateFromDto (this CreateStaffRelateDto createStaffRelateDto)
        {
            return new StaffRelate{
                ID_Staff = createStaffRelateDto.ID_Staff,
                LeaveApplicationID=createStaffRelateDto.LeaveApplicationID,
            };
        }
    }
}