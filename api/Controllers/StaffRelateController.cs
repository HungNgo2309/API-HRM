using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.StaffRelate;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/staffrelate")]
    [ApiController]
    public class StaffRelateController:ControllerBase
    {
        private readonly IStaffRelateReposity _staffRelateRepo;
        private readonly ApplicationDBContext _context;

        public StaffRelateController(ApplicationDBContext context,IStaffRelateReposity staffRelateReposity)
        {
            _staffRelateRepo=staffRelateReposity;
            _context=context;
        } 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staffRealte= await _staffRelateRepo.GetAllAsync();
            var staffRealteDto=staffRealte.Select(s=>s.ToStaffRelateDto());
            return Ok(staffRealteDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var staffRealte=await _staffRelateRepo.GetById(id);
            if(staffRealte==null)
            {
                return NotFound();
            }
            return Ok(staffRealte.ToStaffRelateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStaffRelateDto createStaffRelateDto)
        {
            var staffRelate=createStaffRelateDto.ToCreateFromDto();
            await _staffRelateRepo.CreateAsync(staffRelate);
            return CreatedAtAction(nameof(GetById), new {id=staffRelate.ID},staffRelate.ToStaffRelateDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateStaffRelateDto updateStaff) 
        {
            var staffRelateModel =await _staffRelateRepo.UpdateAsync(id,updateStaff);
            if(staffRelateModel==null)
            {
                return NotFound();
            }
            return Ok(staffRelateModel.ToStaffRelateDto());
        } 
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var staffRelateModel =await _staffRelateRepo.DeleteAsync(id);
            if(staffRelateModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}