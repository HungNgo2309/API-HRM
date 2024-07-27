using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.LeaveApplication;
using api.Models;

namespace api.Mappers
{
    public static class LeaveApplicationMapper
    {
        public  static LeaveApplicationDto ToLeaveApplicationDto(this LeaveApplication leaveApplication)
        {
            return new LeaveApplicationDto{
                ID=leaveApplication.ID,
                Above=leaveApplication.Above,
                AccepterID=leaveApplication.AccepterID,
                Content=leaveApplication.Content,
                CreateDay=leaveApplication.CreateDay,
                Location=leaveApplication.Location,
                ReasonCategoryID=leaveApplication.ReasonCategoryID,
                Reject_Reason=leaveApplication.Reject_Reason,
                State=leaveApplication.State,
                StartDay=leaveApplication.StartDay,
                EndDay=leaveApplication.EndDay,
                StaffID=leaveApplication.StaffID,
                SuperiorID=leaveApplication.SuperiorID,
                AuthorizerID=leaveApplication.AuthorizerID,
                StaffRelates=leaveApplication.StaffRelates.Select(s=>s.ToStaffRelateDto()).ToList()
            };
        }
        public  static LeaveApplication ToCreateFromDto(this CreateLeaveDto createLeaveDto,int idStaff)
        {
            return new LeaveApplication{
                Above=createLeaveDto.Above,
                AccepterID=createLeaveDto.AccepterID,
                Content=createLeaveDto.Content,
                CreateDay=createLeaveDto.CreateDay,
                Location=createLeaveDto.Location,
                ReasonCategoryID=createLeaveDto.ReasonCategoryID,
                Reject_Reason="",
                State=false,
                StartDay=createLeaveDto.StartDay,
                EndDay=createLeaveDto.EndDay,
                StaffID=idStaff,
                AuthorizerID=createLeaveDto.AuthorizerID,
                SuperiorID=createLeaveDto.SuperiorID
            };
        }
    }
}