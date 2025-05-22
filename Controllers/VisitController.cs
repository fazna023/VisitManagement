using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorManagement.Dto;
using VisitorManagement.Models;
using VisitorManagement.Services;

namespace VisitorManagement.Controllers
{
    [ApiController]
    [Route("api/visit")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInDto dto)
        {
            if (dto == null || dto.VisitorId <= 0)
                return BadRequest("Invalid visitor ID.");

            var result = await _visitService.CheckInAsync(dto.VisitorId);
            if (!result)
                return BadRequest("Check-in failed.");

            return Ok("Check-in successful.");
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDto dto)
        {
            if (dto == null || dto.VisitorId <= 0)
                return BadRequest("Invalid visitor ID.");

            var result = await _visitService.CheckOutAsync(dto.VisitorId);
            if (!result)
                return BadRequest("Check-out failed.");

            return Ok("Check-out successful.");
        }

        [HttpGet("today/{hostEmployeeId}")]
        public async Task<ActionResult<List<ViewDto>>> GetTodayVisits(int hostEmployeeId)
        {
            if (hostEmployeeId <= 0)
                return BadRequest("Invalid host employee ID.");

            var visits = await _visitService.GetTodayVisitsAsync(hostEmployeeId);

            var visitDtos = new List<ViewDto>();
            foreach (var v in visits)
            {
                visitDtos.Add(new ViewDto
                {
                    VisitLogId = v.Id,
                    VisitorId = v.VisitorId,
                    VisitorName = v.Visitor?.Name,
                    VisitDate = v.VisitDate,
                    CheckInTime = v.CheckInTime,
                    CheckOutTime = v.CheckOutTime,
                    HostEmployeeId = v.HostEmployeeId,
                    HostEmployeeName = v.HostEmployee?.Name,
                    Purpose = v.Purpose,
                    Status = v.Status,
                    Notes = v.Notes
                });
            }

            return Ok(visitDtos);
        }

        [HttpPut("update")]
        public async Task<IActionResult> ManualUpdate([FromBody] VisitLog visit)
        {
            if (visit == null || visit.Id <= 0)
                return BadRequest("Invalid visit log data.");

            var result = await _visitService.ManualUpdateAsync(visit);
            if (!result)
                return BadRequest("Update failed.");

            return Ok("Visit updated successfully.");
        }

        [HttpGet("noshows/dates")]
        public async Task<IActionResult> GetNoShowDates()
        {
            var dates = await _visitService.GetNoShowDatesAsync();
            return Ok(dates);
        }


        [HttpGet("report")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] int year, [FromQuery] int month)
        {
            var report = await _visitService.GetMonthlyReportAsync(year, month);
            return Ok(report);
        }
    }
}
