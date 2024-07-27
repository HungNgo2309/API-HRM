using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Department;
using api.Models;

namespace api.Interfaces
{
    public interface IDepartmentReposity
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department?> CreateAsync(Department departModel);
        Task<Department?> UpdateAsync(int id,UpdateDepartDto updateDepartDto);
        Task<Department?> DeleteAsync(int id);
    }
}