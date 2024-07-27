using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Position;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PositionReposity : IPositionRepository
    {
        private readonly ApplicationDBContext _context;

        public PositionReposity(ApplicationDBContext context)
        {
            _context =context;
        }
        public async Task<Position?> CreateAsync(Position positionModel)
        {
            await _context.Positions.AddAsync(positionModel);
            await _context.SaveChangesAsync();
            return positionModel;
        }

        public async Task<Position?> DeleteAsync(int id)
        {
            var pos =await _context.Positions.FirstOrDefaultAsync(s=>s.ID==id);
            if(pos==null)
            {
                return null;
            }
            _context.Positions.Remove(pos);
            await _context.SaveChangesAsync();
            return pos;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.Include(c=>c.Staffs).ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(int id)
        {
            return  await _context.Positions.Include(c=>c.Staffs).FirstOrDefaultAsync(i=>i.ID==id);
        }

        public async Task<Position?> UpdateAsync(int id, CreatePositionDto positionDto)
        {
            var existingPosition =await _context.Positions.FirstOrDefaultAsync(s=>s.ID==id);
            if(existingPosition==null)
            {
                return null;
            }
            existingPosition.Role=positionDto.Role;
            existingPosition.Name=positionDto.Name;
            await _context.SaveChangesAsync();
            return existingPosition;
        }
    }
}