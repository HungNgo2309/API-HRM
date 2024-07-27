using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.LeaveApplication;
using api.Models;

namespace api.Interfaces
{
    public interface ILeaveApplication
    {
        Task<List<LeaveApplication>> GetAllAsync();
        Task<LeaveApplication?> GetByIdAsync(int id);
        Task<LeaveApplication?> CreateAsync(LeaveApplication leaveApplication);
        Task<LeaveApplication?> UpdateAsync(int id,UpdateLeaveDto updateLeaveDto);
        Task<LeaveApplication?> DeleteAsync(int id);
        int CheckIsValid(int idStaff);
        Task<List<LeaveApplication>> GetByIdStaffAsync(int staffID);
        Task<List<LeaveApplication>> GetByAccepterIDAsync(int staffID);
    }
}