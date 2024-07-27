using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Position;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/position")]
    [ApiController]
    public class PositionController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPositionRepository _positionRepo;

        public PositionController(ApplicationDBContext context,IPositionRepository positionReposity)
        {
            _context=context;
            _positionRepo=positionReposity;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var position= await _positionRepo.GetAllAsync();
            var positionDto=position.Select(s=>s.ToPositionDto());
            return Ok(positionDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var position=await _positionRepo.GetByIdAsync(id);
            if(position==null)
            {
                return NotFound();
            }
            return Ok(position.ToPositionDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePositionDto positionDto)
        {
            var positionModel=positionDto.FromCreateToDto();
            await _positionRepo.CreateAsync(positionModel);
            return CreatedAtAction(nameof(GetById),new{id=positionModel.ID},positionModel.ToPositionDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] CreatePositionDto positionDto)
        {
            var positionModel =await _positionRepo.UpdateAsync(id,positionDto);
            if(positionModel==null)
            {
                return NotFound();
            }
            return Ok(positionModel.ToPositionDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var positionModel =await _positionRepo.DeleteAsync(id);
            if(positionModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}