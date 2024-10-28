using Dz_identity.Context;
using Dz_identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Dz_identity.Service
{
    public class ProjectService
    {
        private readonly AplicationDbContext _projectContext;

        public ProjectService(AplicationDbContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task AddProjectAsync(Project project)
        {
            await _projectContext.Projects.AddAsync(project);
            await _projectContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectContext.Projects.FindAsync(id);
            if (project != null)
            {
                _projectContext.Projects.Remove(project);
                await _projectContext.SaveChangesAsync();
            }
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await _projectContext.Projects.FindAsync(id); 
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _projectContext.Projects.Update(project);
            await _projectContext.SaveChangesAsync(); 
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _projectContext.Projects.ToListAsync();
        }
    }
}
