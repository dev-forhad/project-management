using project_management_api.Model;

namespace project_management_api.Interface
{
    public interface IProjectRepository
    {
        Task<ProjectInformation> CreateProject(ProjectInformation project);
        Task<ProjectInformation> GetProjectById(int id);
        Task<ProjectInformation> UpdateProject(ProjectInformation project);
    }
}
