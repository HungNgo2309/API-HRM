using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Department;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DepartmentReposity : IDepartmentReposity
    {
        private readonly ApplicationDBContext _context;

        public DepartmentReposity(ApplicationDBContext context)
        {
            _context =context;
        }
        public async Task<Department?> CreateAsync(Department departModel)
        {
            await _context.Departments.AddAsync(departModel);
            await _context.SaveChangesAsync();
            return departModel;
        }

        public async Task<Department?> DeleteAsync(int id)
        {
            var departModel =await _context.Departments.FirstOrDefaultAsync(x => x.ID == id);
            if(departModel==null)
            {
                return null;
            }
            _context.Departments.Remove(departModel);
            await _context.SaveChangesAsync();
            return departModel;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.Include(c=>c.Staffs).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.Include(c=>c.Staffs).FirstOrDefaultAsync(i=>i.ID==id);
        }

        public async Task<Department?> UpdateAsync(int id, UpdateDepartDto updateDepartDto)
        {
            var existingDepart =await _context.Departments.FirstOrDefaultAsync(x => x.ID == id);
            if(existingDepart==null)
            {
                return null;
            }
            existingDepart.Name=updateDepartDto.Name;
            await _context.SaveChangesAsync();
            return existingDepart;
        }

    }
}