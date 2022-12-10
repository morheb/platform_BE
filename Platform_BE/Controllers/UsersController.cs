using ConnectMediaEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform_BE.Model;

namespace ConnectMediaEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context) => _context = context;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFireBaseId(string FireBaswid)
        {
            var Dbuser = _context.Users.Where(u => u.FirebaseId == FireBaswid).FirstOrDefault();
            return Dbuser == null ? NotFound() : Ok(Dbuser);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetalluserDepartment(string Department)
        {
            var Dbuser = _context.Users.Where(u => u.Department == Department);
            return Dbuser == null ? NotFound() : Ok(Dbuser);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Getalluser()
        {
            var Dbuser =await _context.Users.FromSqlRaw("Select * From Users").ToListAsync();
            return Dbuser == null ? NotFound() : Ok(Dbuser);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegesterUser(User user)
        {
            var Dbuser = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (Dbuser != null) return BadRequest();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Updateuser(User user)
        {
            var Dbuser = await _context.Users.FindAsync(user.Id);
            if (Dbuser == null) return BadRequest();

            Dbuser.FirebaseId = user.FirebaseId;
            Dbuser.UserName = user.UserName;
            Dbuser.Email = user.Email;
            Dbuser.UserPhone = user.UserPhone;
            Dbuser.UserImage = user.UserImage;
            Dbuser.UserRank = user.UserRank;
            Dbuser.AlluserTask = user.AlluserTask;
            Dbuser.PendinguserTask = user.PendinguserTask;
            Dbuser.UserAbout = user.UserAbout;
            Dbuser.Usertype = user.Usertype;
            Dbuser.UserAge = user.UserAge;
            Dbuser.Department = user.Department;
            Dbuser.FireBaseToken = user.FireBaseToken;

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
