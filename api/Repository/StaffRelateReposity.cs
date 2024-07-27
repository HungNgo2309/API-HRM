using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.StaffRelate;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StaffRelateReposity : IStaffRelateReposity
    {
        private readonly IEmailService _emailRepo;
        private readonly IStaffRepository _staffRepo;
        private readonly ApplicationDBContext _context;

        public StaffRelateReposity(ApplicationDBContext context,IEmailService emailService,IStaffRepository staffRepository)
        {
            _emailRepo= emailService;
            _staffRepo= staffRepository;
            _context = context;
        }
        public async Task<StaffRelate?> CreateAsync(StaffRelate staffRelate)
        {
            // Add the staffRelate to the context and save changes
            await _context.StaffRelates.AddAsync(staffRelate);
            await _context.SaveChangesAsync();
            await _emailRepo.EmailForStaffRelate(staffRelate);
            return staffRelate;
        }


        public async Task<StaffRelate?> DeleteAsync(int id)
        {
            var staffrelateModel =await _context.StaffRelates.FirstOrDefaultAsync(x => x.ID == id);
            if(staffrelateModel==null)
            {
                return null;
            }
            _context.StaffRelates.Remove(staffrelateModel);
            await _context.SaveChangesAsync();
            return staffrelateModel;
        }

        public async Task<List<StaffRelate>> GetAllAsync()
        {
            return await _context.StaffRelates.ToListAsync();
        }

        public async Task<StaffRelate?> GetById(int id)
        {
            return await _context.StaffRelates.FindAsync(id);
        }

        public async Task<StaffRelate?> UpdateAsync(int id,UpdateStaffRelateDto updateStaffRelateDto)
        {
            var existStaffRL =await _context.StaffRelates.FirstOrDefaultAsync(x => x.ID == id);
            if(existStaffRL==null)
            {
                return null;
            }
            existStaffRL.ID_Staff=updateStaffRelateDto.ID_Staff;
            existStaffRL.LeaveApplicationID=updateStaffRelateDto.LeaveApplicationID;
            await _context.SaveChangesAsync();
            await _emailRepo.EmailForStaffRelate(existStaffRL);
            return existStaffRL;
        }
    }
}