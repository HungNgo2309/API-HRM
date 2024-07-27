using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTO.LeaveApplication;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LeaveApplicationReposity:ILeaveApplication
    {
        private readonly ApplicationDBContext _context;
        private readonly IStaffRepository _staffRepo;
        private readonly IEmailService _emailRepo;

        public LeaveApplicationReposity(ApplicationDBContext context,IStaffRepository staffRepository,IEmailService emailService)
        {
            _context =context;
            _staffRepo = staffRepository;
            _emailRepo =emailService;
        }

        public int CheckIsValid(int idStaff)
        {
            var staffremain = _context.LeaveApplications.Where(c=>c.StaffID==idStaff).ToList();
            if(staffremain!=null)
            {
                int sl = 0;
                for (int i = 0; i < staffremain.Count; i++)
                {
                    TimeSpan duration = staffremain[i].EndDay - staffremain[i].StartDay;
                    sl=sl+ duration.Days+1; // tính số ngày
                }
                return sl;
            }
            return 0;
        }

      public async Task<LeaveApplication?> CreateAsync(LeaveApplication leaveApplication)
        {
            await _context.LeaveApplications.AddAsync(leaveApplication);
            await _context.SaveChangesAsync();
            await _emailRepo.SendEmailInfo(leaveApplication);
            return leaveApplication;
        }
        public async Task<LeaveApplication?> DeleteAsync(int id)
        {
            var leaveModel =await _context.LeaveApplications.FirstOrDefaultAsync(x => x.ID == id);
            if(leaveModel==null)
            {
                return null;
            }
            _context.LeaveApplications.Remove(leaveModel);
            await _context.SaveChangesAsync();
            return leaveModel;
        }

        public async Task<List<LeaveApplication>> GetAllAsync()
        {
            return await _context.LeaveApplications.Include(c=>c.StaffRelates).ToListAsync();
        }

        public async Task<List<LeaveApplication>> GetByAccepterIDAsync(int staffID)
        {
            return await _context.LeaveApplications.Where(s=>s.AccepterID==staffID&& s.State==false)
            .Include(c=>c.StaffRelates).ToListAsync();
        }

        public async Task<LeaveApplication?> GetByIdAsync(int id)
        {
             return await _context.LeaveApplications.Include(c=>c.StaffRelates).FirstOrDefaultAsync(i=>i.ID==id);
        }

        public async Task<List<LeaveApplication>> GetByIdStaffAsync(int staffID)
        {
            return await _context.LeaveApplications.Where(s=>s.StaffID==staffID).Include(c=>c.StaffRelates).ToListAsync();
        }

        public async Task<LeaveApplication?> UpdateAsync(int id, UpdateLeaveDto updateLeaveDto)
        {
            var existingLeave =await _context.LeaveApplications.FirstOrDefaultAsync(x => x.ID == id);
            if(existingLeave==null)
            {
                return null;
            }
            var check =existingLeave.State;
            existingLeave.Location=updateLeaveDto.Location;
            existingLeave.Above=updateLeaveDto.Above;
            existingLeave.AccepterID=updateLeaveDto.AccepterID;
            existingLeave.Content=updateLeaveDto.Content;
            existingLeave.StartDay=updateLeaveDto.StartDay;
            existingLeave.EndDay=updateLeaveDto.EndDay;
            existingLeave.StaffID=updateLeaveDto.StaffID;
            existingLeave.CreateDay=DateTime.Now;
            existingLeave.ReasonCategoryID=updateLeaveDto.ReasonCategoryID;
            existingLeave.Reject_Reason=updateLeaveDto.Reject_Reason;
            existingLeave.State=updateLeaveDto.State;
            existingLeave.SuperiorID=updateLeaveDto.SuperiorID;
            existingLeave.AuthorizerID=updateLeaveDto.AuthorizerID;
            await _context.SaveChangesAsync();
            if(existingLeave.State!=check)
            {
                await _emailRepo.SendEmailInfo(existingLeave);
                var staffRelates =await _context.StaffRelates.Where(c=>c.LeaveApplicationID==existingLeave.ID).ToListAsync();
                for(int i=0;i<staffRelates.Count;i++)
                {
                    await _emailRepo.EmailForStaffRelate(staffRelates[i]);
                }      
            }
            return existingLeave;
        }
    }
}