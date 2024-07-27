using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.LeaveApplication;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/leaveapplication")]
    [ApiController]
    public class LeaveApplicationController:ControllerBase
    {
        
        private readonly ILeaveApplication _leaveRepo;
        private readonly ApplicationDBContext _context;
        private readonly IEmailService _emailservice;

        public LeaveApplicationController(ApplicationDBContext context,ILeaveApplication leaveApplication,IEmailService emailService)
        {
            _leaveRepo=leaveApplication;
            _context=context;
            _emailservice=emailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leave= await _leaveRepo.GetAllAsync();
            var leaveDto=leave.Select(s=>s.ToLeaveApplicationDto());
            return Ok(leaveDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var leavement=await _leaveRepo.GetByIdAsync(id);
            if(leavement==null)
            {
                return NotFound();
            }
            return Ok(leavement.ToLeaveApplicationDto());
        }
        [HttpPost("{idStaff}")]
        public async Task<IActionResult> Create([FromRoute] int idStaff,[FromBody] CreateLeaveDto createLeaveDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TimeSpan duration = createLeaveDto.EndDay - createLeaveDto.StartDay;
            // int remainDay = _leaveRepo.CheckIsValid(idStaff);
            // if(remainDay+duration.TotalDays+1>12)
            // {
            //     return BadRequest("Thời gian xin nghỉ vượt quá thời gian quy định");
            // }
            var leave=createLeaveDto.ToCreateFromDto(idStaff);
            await _leaveRepo.CreateAsync(leave);
            return CreatedAtAction(nameof(GetById), new {id=leave.ID},leave.ToLeaveApplicationDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateLeaveDto updateLeaveDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var leaveModel =await _leaveRepo.UpdateAsync(id,updateLeaveDto);
            if(leaveModel==null)
            {
                return NotFound();
            }
            return Ok(leaveModel.ToLeaveApplicationDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var leaveModel =await _leaveRepo.DeleteAsync(id);
            if(leaveModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("GetByIdStaff/{idStaff:int}")]
        public async Task<IActionResult> GetByIdStaff ([FromRoute] int idStaff)
        {
            var listLeave =await _leaveRepo.GetByIdStaffAsync(idStaff);
            var leaveDto=listLeave.Select(s=>s.ToLeaveApplicationDto());
            return Ok(leaveDto);
        }
        [HttpGet("GetByAccepterID/{accepterID:int}")]
        public async Task<IActionResult> GetByAccepterID ([FromRoute] int accepterID)
        {
            var listLeave =await _leaveRepo.GetByAccepterIDAsync(accepterID);
            var leaveDto=listLeave.Select(s=>s.ToLeaveApplicationDto());
            return Ok(leaveDto);
        }
    }
}