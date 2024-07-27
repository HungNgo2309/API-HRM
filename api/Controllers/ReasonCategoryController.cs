using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ReasonCategory;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/reasoncategory")]
    [ApiController]
    public class ReasonCategoryController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IReasonReposity _reasonRepo;

        public ReasonCategoryController(ApplicationDBContext context,IReasonReposity reasonReposity)
        {
            _context= context;
            _reasonRepo = reasonReposity;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reasonModel= await _reasonRepo.GetAllAsync();
            var reasonDto=reasonModel.Select(s=>s.ToReasonDto());
            return Ok(reasonDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var reason=await _reasonRepo.GetByIdAsync(id);
            if(reason==null)
            {
                return NotFound();
            }
            return Ok(reason.ToReasonDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReasonChangeDto reasonDto)
        {
            var reasonModel=reasonDto.FromCreateToDto();
            await _reasonRepo.CreateAsync(reasonModel);
            return CreatedAtAction(nameof(GetById),new{id=reasonModel.ID},reasonModel.ToReasonDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] ReasonChangeDto reasonDto)
        {
            var reasonModel =await _reasonRepo.UpdateAsync(id,reasonDto);
            if(reasonModel==null)
            {
                return NotFound();
            }
            return Ok(reasonModel.ToReasonDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var reasonModel =await _reasonRepo.DeleteAsync(id);
            if(reasonModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}