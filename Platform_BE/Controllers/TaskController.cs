using ConnectMediaEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform_BE.Model;

namespace ConnectMediaEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context) => _context = context;


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTask(Tasks userTask)
        {
            var Dbuser = _context.UserTasks.Where(u => u.selectedDay == userTask.selectedDay).FirstOrDefault();
            if(Dbuser == null)
            {
                userTasks newUserTask = new userTasks()
                {
                    selectedDay = userTask.selectedDay,
                    userId = userTask.userId,
                };

                await _context.UserTasks.AddAsync(newUserTask);
                await _context.SaveChangesAsync();
            }
            Dbuser = _context.UserTasks.Where(u => u.selectedDay == userTask.selectedDay).FirstOrDefault();
            await _context.Tasks.AddAsync(userTask);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDay(userTasks userTask)
        {
            List<userTasks> alltask = new List<userTasks>();
            var user = await _context.Users.FindAsync(userTask.userId);
            var result = await _context.UserTasks.FromSqlRaw("Select * From userTasks WHERE selectedDay >= {0}", userTask.selectedDay).ToListAsync();
            foreach(var newtask in result)
            {
                var task = await _context.Tasks.FromSqlRaw("Select * From Tasks WHERE selectedDay = {0} AND userId IN (0 , {1}) AND Department = {2}", newtask.selectedDay , userTask.userId , user.Department).ToListAsync();
                userTasks newUserTask = new userTasks()
                {
                    selectedDay = newtask.selectedDay,
                    tasks = task
                };
                alltask.Add(newUserTask);
            }
            if (alltask.Count < 0) return NotFound();
            return Ok(alltask);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(Tasks task)
        {
            var Dbtask = await _context.Tasks.FindAsync(task.id);
            if (Dbtask == null) return BadRequest();
            Dbtask.doneOrNot = true;
            await _context.SaveChangesAsync();
            var alltask = await _context.Tasks.FromSqlRaw("Select * From Tasks WHERE ProjectId = {0}", task.ProjectId).ToListAsync();
            var newpercentage = (1 / (alltask.Count * 1.0)) *100;
            var Project = await _context.Projects.FindAsync(task.ProjectId);
            Project.PercentageComplete = Project.PercentageComplete + newpercentage;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
