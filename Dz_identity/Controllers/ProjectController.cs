using Dz_identity.Models;
using Dz_identity.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dz_identity.Controllers
{
    public class ProjectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ProjectService  _projecktService;
        private readonly TaskService _taskService;

        public ProjectController(UserManager<IdentityUser> userManager, ProjectService projectService)
        {
            _userManager = userManager;
            _projecktService = projectService;
        }
        public async Task<IActionResult> Read()
        {
            var projects = await _projecktService.GetProjectsAsync();
            return View(projects); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
            { 
                project.OwnerId = _userManager.GetUserId(User); 
                project.Owner = await _userManager.GetUserAsync(User);
                Console.WriteLine($"Owner ID: {project.OwnerId}");
                await _projecktService.AddProjectAsync(project);
                return RedirectToAction("Read"); 
            }
            return View(project);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projecktService.GetProjectAsync(id);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string name)
        {
            await _projecktService.DeleteProjectAsync(id);
            return RedirectToAction("Read"); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projecktService.GetProjectAsync(id);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Project project)
        {
            if (!ModelState.IsValid)
            {
                project.OwnerId = _userManager.GetUserId(User);
                project.Owner = await _userManager.GetUserAsync(User);
                Console.WriteLine($"Owner ID: {project.OwnerId}");
                await _projecktService.UpdateProjectAsync(project);
                return RedirectToAction("Read");
            }
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _projecktService.GetProjectAsync(id);
            return View(project); 
        }

        [HttpGet]
        public IActionResult ViewTasks(int id)
        {
            GetProjectId.project.ProjectId = id;
            return RedirectToAction("Read", "Task");
        }
    }
}
