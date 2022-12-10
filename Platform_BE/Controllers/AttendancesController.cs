using ConnectMediaEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform_BE.Model;
using System.Threading.Tasks;

namespace ConnectMediaEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendancesController(AppDbContext context) => _context = context;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var dayInfo = await _context.Attendances.FindAsync(id);
            return dayInfo == null ? NotFound() : Ok(dayInfo);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAlluserattendance(int userid)
        {
            var result = await _context.Attendances.FromSqlRaw("Select * From Attendances WHERE userId = {0}", userid).ToListAsync();
            if (result.Count > 0) return Ok(result);
            return NotFound();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDay(Attendance attendance)
        {
            var result = await _context.Attendances.FromSqlRaw("Select * From Attendances WHERE userId = {0} AND attendanceDay = {1}", attendance.userId , attendance.AttendanceDay).ToListAsync();
            if(result.Count > 0) return Ok(result);
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance == null ? NotFound() : Ok(attendance);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAlluserByDay()
        {
            var result = await _context.Attendances.FromSqlRaw("Select * From Attendances").ToListAsync();
            return Ok(result);
        }
        /*[HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAttendances(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance==null?NotFound():Ok();
        }*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> ShowAttendanceForUser(int userid)
        {
            var result =await _context.Attendances.FromSqlRaw("Select * From Attendances WHERE userId = {0}", userid).ToListAsync();
            return result == null ? NotFound():Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttendances(Attendance attendance)
        {
            var Dbattendance = await _context.Attendances.FindAsync(attendance.Id);
            if (Dbattendance == null) return BadRequest();
            Dbattendance.AttendancestartTime = attendance.AttendancestartTime;
            Dbattendance.AttendanceendTime = attendance.AttendanceendTime;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
