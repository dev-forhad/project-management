using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_api.Interface;
using project_management_api.Model;
using System.Security.Claims;

namespace project_management_api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectInformation>> CreateProject([FromBody] ProjectInformation project)
        {
            var adminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var admin = await _userRepository.FindByName(adminId);
            

            project.Admin = admin;
            var createdProject = await _projectRepository.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectInformation>> AssignDevelopers(int id, [FromBody] IEnumerable<string> developerIds)
        {
            var adminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var admin = await _userRepository.FindByName(adminId);
          

            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            var developers = await _userRepository.GetUsersByIds(developerIds);
            //if (developers.Any(d => d.r != "Developer"))
            //{
            //    return BadRequest("Cannot assign non-developer as developer.");
            //}

            project.Developers = developers;
            var updatedProject = await _projectRepository.UpdateProject(project);
            return Ok(updatedProject);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectInformation>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
    }

}
