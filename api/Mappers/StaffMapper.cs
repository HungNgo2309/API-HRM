using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Staff;
using api.DTOs.Staff;
using api.Models;

namespace api.Mappers
{
    public static class StaffMapper
    {
        public  static StaffDTO ToStaffDto(this Staff staffModel)
        {
            return new StaffDTO{
                ID=staffModel.ID,
                Name= staffModel.Name,
                Email=staffModel.Email,
                Phone=staffModel.Phone,
                Password=staffModel.Password,
                TelegramID=staffModel.TelegramID,
                DepartmentID=staffModel.DepartmentID,
                PositionID=staffModel.PositionID,
                LeaveApplications =staffModel.LeaveApplications.Select(s=>s.ToLeaveApplicationDto()).ToList()
            };
        }
        public static Staff ToStaffFromCreateDTO(this CreateStaffRequestDto staffDto,int IdDepartment,int IdPosition)
        {
            return new Staff{
                Name=staffDto.Name,
                Email=staffDto.Email,
                Password=staffDto.Password,
                Phone=staffDto.Phone,
                TelegramID=staffDto.TelegramID,
                DepartmentID=IdDepartment,
                PositionID=IdPosition,
            };
        }
    }
}