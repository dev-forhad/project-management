using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_api.DTO;
using project_management_api.Interface;
using project_management_api.Model;

namespace project_management_api.Controllers
{
    [Authorize(Roles = "Developer")]
    [Route("api/")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class TaskInformationController : ControllerBase
    {
        private readonly ITaskInformationRepository _taskInformationRepository;

        public TaskInformationController(ITaskInformationRepository taskInformationRepository)
        {
            _taskInformationRepository = taskInformationRepository;
        }

        [HttpPost]
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] TaskInformationDto taskInformation)
        {
            var task = new TaskInformation
            {
                Id = projectId,
                Name = taskInformation.Name,
                StartDate = taskInformation.StartDate,
                EndDate = taskInformation.EndDate,
                Difficulty = taskInformation.Difficulty,
                Status = taskInformation.Status
            };

            await _taskInformationRepository.CreateTask(task);

            return CreatedAtAction(nameof(GetTaskById), new { taskId = task.Id }, task);
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(int projectId, int taskId, [FromBody] TaskInformationDto taskInformation)
        {
            var task = await _taskInformationRepository.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }

            task.Name = taskInformation.Name;
            task.StartDate = taskInformation.StartDate;
            task.EndDate = taskInformation.EndDate;
            task.Difficulty = taskInformation.Difficulty;
            task.Status = taskInformation.Status;

            await _taskInformationRepository.UpdateTask(task);

            return NoContent();
        }

        [HttpGet]
        [Route("GetTaskById")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _taskInformationRepository.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}
