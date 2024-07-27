using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Staff;
using api.Models;

namespace api.Interfaces
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff> CreateAsync(Staff staffModel);
        Task<Staff?> UpdateAsync(int id, UpdateStaffRequestDto staffRequestDto);
        Task<Staff?> DeleteAsync(int id);
        Task<List<Staff>?> FindSuperior(int id);
        Task<Staff> GetByEmailAndPasswordAsync(string email, string password);
    }
}