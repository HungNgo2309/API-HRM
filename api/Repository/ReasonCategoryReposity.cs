using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ReasonCategory;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReasonCategoryReposity : IReasonReposity
    {
        private readonly ApplicationDBContext _context;

        public ReasonCategoryReposity(ApplicationDBContext context)
        {
            _context =context;
        }
        public async Task<ReasonCategory?> CreateAsync(ReasonCategory reasonCategory)
        {
            await _context.ResonCategorys.AddAsync(reasonCategory);
            await _context.SaveChangesAsync();
            return reasonCategory;
        }

        public async Task<ReasonCategory?> DeleteAsync(int id)
        {
            var delete = await _context.ResonCategorys.FirstOrDefaultAsync(i=>i.ID==id);
            if(delete==null)
            {
                return null;
            }
            _context.ResonCategorys.Remove(delete);
            await _context.SaveChangesAsync();
            return delete;
        }

        public async Task<List<ReasonCategory>> GetAllAsync()
        {
            return await _context.ResonCategorys.ToListAsync();
        }

        public async Task<ReasonCategory?> GetByIdAsync(int id)
        {
            return await _context.ResonCategorys.FirstOrDefaultAsync(n=>n.ID==id);
        }

        public async Task<ReasonCategory?> UpdateAsync(int id, ReasonChangeDto reasonChangeDto)
        {
            var exists = await _context.ResonCategorys.FirstOrDefaultAsync(i=>i.ID==id);
            if(exists==null)
            {
                return null;
            }
            exists.Name=reasonChangeDto.Name;
            await _context.SaveChangesAsync();
            return exists;
        }
    }
}