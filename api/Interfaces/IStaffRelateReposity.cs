using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.StaffRelate;
using api.Models;

namespace api.Interfaces
{
    public interface IStaffRelateReposity
    {
        Task<List<StaffRelate>> GetAllAsync();
        Task<StaffRelate?> GetById(int id);
        Task<StaffRelate?> CreateAsync(StaffRelate staffRelate);
        Task<StaffRelate?> UpdateAsync(int id,UpdateStaffRelateDto updateStaffRelateDto);
        Task<StaffRelate?> DeleteAsync(int id);
    }
}