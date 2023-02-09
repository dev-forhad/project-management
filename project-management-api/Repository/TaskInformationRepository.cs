using Microsoft.EntityFrameworkCore;
using project_management_api.Data;
using project_management_api.Interface;
using project_management_api.Model;

namespace project_management_api.Repository
{
    public class TaskInformationRepository : ITaskInformationRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskInformationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskInformation> GetTaskById(int taskInformationId)
        {
            return await _context.TaskInformations.FindAsync(taskInformationId);
        }

        public async Task<IEnumerable<TaskInformation>> GetTaskInformations()
        {
            return await _context.TaskInformations.ToListAsync();
        }

        public async Task CreateTask(TaskInformation taskInformation)
        {
            await _context.TaskInformations.AddAsync(taskInformation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskInformation taskInformation)
        {
            _context.TaskInformations.Update(taskInformation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int taskInformationId)
        {
            var taskInformation = await _context.TaskInformations.FindAsync(taskInformationId);
            _context.TaskInformations.Remove(taskInformation);
            await _context.SaveChangesAsync();
        }
    }
}
