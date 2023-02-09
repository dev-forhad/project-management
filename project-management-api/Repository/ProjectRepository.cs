using project_management_api.Data;
using project_management_api.Interface;
using project_management_api.Model;

namespace project_management_api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectInformation> CreateProject(ProjectInformation project)
        {
            await _context.ProjectInformation.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<ProjectInformation> GetProjectById(int id)
        {
            return await _context.ProjectInformation.FindAsync(id);
        }

        public async Task<ProjectInformation> UpdateProject(ProjectInformation project)
        {
            _context.ProjectInformation.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }

}
