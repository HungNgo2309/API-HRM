using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Staff;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController:ControllerBase
    {
        private readonly IStaffRepository _staffRepo;
        private readonly ApplicationDBContext _context;

        public StaffController(ApplicationDBContext context,IStaffRepository staffRepo)
        {
            _staffRepo=staffRepo;
            _context=context;
        } 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staff= await _staffRepo.GetAllAsync();
            var staffDto=staff.Select(s=>s.ToStaffDto());
            return Ok(staffDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var staff=await _staffRepo.GetByIdAsync(id);
            if(staff==null)
            {
                return NotFound();
            }
            return Ok(staff.ToStaffDto());
        }
        [HttpGet("FindSuperior/{id:int}")]
        public async Task<IActionResult> FindSuperior([FromRoute] int id)
        {
            var staff=await _staffRepo.FindSuperior(id);
            if(staff==null)
            {
                return NotFound();
            }
            var staffDto=staff.Select(s=>s.ToStaffDto());
            return Ok(staffDto);
        }
        [HttpPost("{iddepart}/{positionId}")]
        public async Task<IActionResult> Create([FromRoute] int iddepart,int positionId,[FromBody] CreateStaffRequestDto staffDto)
        {
            var staff=staffDto.ToStaffFromCreateDTO(iddepart,positionId);
            await _staffRepo.CreateAsync(staff);
            return CreatedAtAction(nameof(GetById), new {id=staff.ID},staff.ToStaffDto());
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateStaffRequestDto updateStaff) 
        {
            var staffModel =await _staffRepo.UpdateAsync(id,updateStaff);
            if(staffModel==null)
            {
                return NotFound();
            }
            return Ok(staffModel.ToStaffDto());
        } 
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var staffModel =await _staffRepo.DeleteAsync(id);
            if(staffModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("GetByEmailAndPassword")]
        public async Task<IActionResult> GetByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
        {
            var staff = await _staffRepo.GetByEmailAndPasswordAsync(email, password);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff.ToStaffDto());
        }
    }
}