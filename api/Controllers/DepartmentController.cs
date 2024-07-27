using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Department;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController:ControllerBase
    {
        private readonly IDepartmentReposity _departRepo;
        private readonly ApplicationDBContext _context;

        public DepartmentController(ApplicationDBContext context,IDepartmentReposity departmentRepo)
        {
            _departRepo=departmentRepo;
            _context=context;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var depart= await _departRepo.GetAllAsync();
            var departDto=depart.Select(s=>s.ToDepartmentDto());
            return Ok(departDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var department=await _departRepo.GetByIdAsync(id);
            if(department==null)
            {
                return NotFound();
            }
            return Ok(department.ToDepartmentDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartDto DepartDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departModel=DepartDto.FromCreateToDto();
            await _departRepo.CreateAsync(departModel);
            return CreatedAtAction(nameof(GetById),new{id=departModel.ID},departModel.ToDepartmentDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateDepartDto departDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DepartModel =await _departRepo.UpdateAsync(id,departDto);
            if(DepartModel==null)
            {
                return NotFound();
            }
            return Ok(DepartModel.ToDepartmentDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var departModel =await _departRepo.DeleteAsync(id);
            if(departModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}