using Dz_identity.Context;
using Dz_identity.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;

namespace Dz_identity.Service
{
    public class TaskService
    {
        private readonly AplicationDbContext _aplicationDbContext;
        public TaskService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public async Task AddTaskAsync(TaskModel task)
        {
            await _aplicationDbContext.Tasks.AddAsync(task);
            await _aplicationDbContext.SaveChangesAsync();
        }
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _aplicationDbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _aplicationDbContext.Tasks.Remove(task);
                await _aplicationDbContext.SaveChangesAsync();
            }
        }
        public async Task<TaskModel> GetTaskAsync(int id)
        {
            return await _aplicationDbContext.Tasks.FindAsync(id);
        }

        public async Task UpdateTaskAsync(TaskModel task)
        {
            _aplicationDbContext.Tasks.Update(task);
            await _aplicationDbContext.SaveChangesAsync();
        }
        public async Task<List<TaskModel>> GetTasksAsync(int id)
        {
            return await _aplicationDbContext.Tasks.Where(c=> c.ProjectId == id).ToListAsync();
        }
    }
}
