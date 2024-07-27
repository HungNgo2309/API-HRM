using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Position;
using api.Models;

namespace api.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(int id);
        Task<Position?> CreateAsync(Position positionModel);
        Task<Position?> UpdateAsync(int id,CreatePositionDto positionDto);
        Task<Position?> DeleteAsync(int id);
    }
}