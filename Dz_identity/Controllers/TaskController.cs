using Dz_identity.Models;
using Dz_identity.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dz_identity.Controllers
{
    public class TaskController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ProjectService _projecktService;
        private readonly TaskService _taskService;

        public TaskController(UserManager<IdentityUser> userManager, ProjectService projectService, TaskService taskService)
        {
            _userManager = userManager;
            _projecktService = projectService;
            _taskService = taskService;
        }
        public async Task<IActionResult> Read()
        {
            Console.WriteLine(GetProjectId.project.ProjectId);
            var tasks = await _taskService.GetTasksAsync(GetProjectId.project.ProjectId);
            return View(tasks);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                task.ProjectId =  GetProjectId.project.ProjectId;
                await _taskService.AddTaskAsync(task);
                return RedirectToAction("Read");
            }
            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string name)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction("Read");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                task.ProjectId = GetProjectId.project.ProjectId;
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("Read");
            }
            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            return View(task);
        }
    }
}
