using project_management_api.Model;

namespace project_management_api.Interface
{
    public interface ITaskInformationRepository
    {
        Task<TaskInformation> GetTaskById(int taskInformationId);
        Task<IEnumerable<TaskInformation>> GetTaskInformations();
        Task CreateTask(TaskInformation taskInformation);
        Task UpdateTask(TaskInformation taskInformation);
        Task DeleteTask(int taskInformationId);
    }
}
