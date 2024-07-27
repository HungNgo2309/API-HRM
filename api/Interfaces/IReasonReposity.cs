using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ReasonCategory;
using api.Models;

namespace api.Interfaces
{
    public interface IReasonReposity
    {
        Task<List<ReasonCategory>> GetAllAsync();
        Task<ReasonCategory?> GetByIdAsync(int id);
        Task<ReasonCategory?> CreateAsync(ReasonCategory reasonCategory);
        Task<ReasonCategory?> UpdateAsync(int id, ReasonChangeDto reasonChangeDto);
        Task<ReasonCategory?> DeleteAsync(int id);
    }
}