using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Staff;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDBContext _context;

        public StaffRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Staff> CreateAsync(Staff staffModel)
        {
            await _context.Staffs.AddAsync(staffModel);
            await _context.SaveChangesAsync();
            return staffModel;
        }

        public async Task<Staff?> DeleteAsync(int id)
        {
            var staffModel =await _context.Staffs.FirstOrDefaultAsync(x => x.ID == id);
            if(staffModel==null)
            {
                return null;
            }
            _context.Staffs.Remove(staffModel);
            await _context.SaveChangesAsync();
            return staffModel;
        }

        public async Task<List<Staff>?> FindSuperior(int id)
        {
            var staff = await _context.Staffs
                                    .Include(s => s.Position)
                                    .FirstOrDefaultAsync(x => x.ID == id);
            if (staff == null || staff.Position == null)
            {
                return null;
            }
            var staffModel = await _context.Staffs
                            .Where(s =>  s.Position != null
                             && s.Position.Role == staff.Position.Role + 1)
                            .ToListAsync();
            //s.DepartmentID == staff.DepartmentID &&
            return staffModel;
        }


        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs.Include(c=>c.LeaveApplications).ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staffs.Include(c=>c.LeaveApplications).FirstOrDefaultAsync(i=>i.ID==id);
        }

        public async Task<Staff?> UpdateAsync(int id, UpdateStaffRequestDto staffRequestDto)
        {
            var existingStaff =await _context.Staffs.FirstOrDefaultAsync(x => x.ID == id);
            if(existingStaff==null)
            {
                return null;
            }
            existingStaff.Name=staffRequestDto.Name;
            existingStaff.Email=staffRequestDto.Email;
            existingStaff.Phone=staffRequestDto.Phone;
            existingStaff.Password=staffRequestDto.Password;
            existingStaff.DepartmentID=staffRequestDto.DepartmentID;
            existingStaff.PositionID=staffRequestDto.PositionID;
            existingStaff.TelegramID=staffRequestDto.TelegramID;
            await _context.SaveChangesAsync();
            return existingStaff;
        }
        public async Task<Staff> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
        }
    }
}