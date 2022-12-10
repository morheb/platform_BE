using ConnectMediaEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform_BE.Model;

namespace ConnectMediaEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context) => _context = context;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Project = await _context.Projects.FindAsync(id);
            var task = await _context.Tasks.FromSqlRaw("Select * From Tasks WHERE ProjectId = {0}",id).ToListAsync();
            Project.tasks = task;
            return Project == null ? NotFound() : Ok(Project);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDepartment(string department)
        {
            var result = await _context.Projects.FromSqlRaw("Select * From Projects WHERE Department = {0}", department).ToListAsync();
            return result == null ? NotFound() : Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByuserId(Project project)
        {
            var task = await _context.Tasks.FromSqlRaw("Select * From Tasks WHERE ProjectId = {0} AND userId IN (0 , {1})", project.Id, project.userId).ToListAsync();
            var Project = await _context.Projects.FindAsync(project.Id);
            Project.tasks = task;
            return Project == null ? NotFound() : Ok(Project);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult>CreateProject(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

    }
}
