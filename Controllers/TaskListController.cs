using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace ArtSkills.Controllers
{
    public class TaskListController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext applicationDbContext { get; }
        private readonly IHostingEnvironment _environment;

        public TaskListController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
        IHostingEnvironment environment)
        {
            _userManager = userManager;
            applicationDbContext = context;
            _environment = environment;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult TaskList(string taskListID)
        {
            TaskList taskList = applicationDbContext.TaskLists.Find(taskListID);
            return View(taskList);
        }

        public IActionResult EditTaskList(string taskListID)
        {
            TaskList taskList = applicationDbContext.TaskLists.Find(taskListID);
            return View(taskList);
        }

        public IActionResult TaskListsCollection(string Id, bool completed)
        {
            ApplicationUser user = applicationDbContext.Users.Find(Id);
            List<TaskList> taskslists;
            if (completed)
                taskslists = user.TaskLists.ToList().FindAll(x => x.Status);
            else
                taskslists = user.TaskLists.ToList().FindAll(x => !x.Status);
            return View(taskslists);
        }

        public async Task<IActionResult> Completed(string Id)
        {
            return RedirectToAction("TaskListsCollection", "TaskList", new { Id, completed = true });
        }

        public async Task<IActionResult> InProgress(string Id)
        {
            return RedirectToAction("TaskListsCollection", "TaskList", new { Id, completed = false });
        }

        public async Task<IActionResult> DeleteTaskList(string taskListID)
        {
            var taskList = applicationDbContext.TaskLists.ToList().Find(x => x.Id == taskListID);
            string userId = taskList.ApplicationUser.Id;
            applicationDbContext.TaskLists.Remove(taskList);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("TaskListIndex", "UserWall", new { Id = userId });
        }

        public async Task<IActionResult> AddTask(string taskListID, string name)
        {
            TaskList taskList = applicationDbContext.TaskLists.Find(taskListID);
            ArtSkills.Models.Task task = new ArtSkills.Models.Task(name, taskList);
            taskList.Tasks.Add(task);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("EditTaskList", "TaskList", new { taskListID });
        }

        public async Task<IActionResult> DeleteTask(string taskId, string taskListID)
        {
            var taskList = applicationDbContext.TaskLists.ToList().Find(x => x.Id == taskListID);
            var task = taskList.Tasks.Find(x => x.Id == taskId);
            taskList.Tasks.Remove(task);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("EditTaskList", "TaskList", new { taskListID });
        }

        public async Task<IActionResult> CheckTask(string taskId, string taskListID)
        {
            var taskList = applicationDbContext.TaskLists.ToList().Find(x => x.Id == taskListID);
            ApplicationUser user = await GetCurrentUserAsync();
            if (user.Id == taskList.ApplicationUser.Id)
            {
                var task = taskList.Tasks.Find(x => x.Id == taskId);
                task.Status = true;
                taskList.Completed++;
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("TaskList", "TaskList", new { taskListID });
        }

        public async Task<IActionResult> UncheckTask(string taskId, string taskListID)
        {
            var taskList = applicationDbContext.TaskLists.ToList().Find(x => x.Id == taskListID);
            ApplicationUser user = await GetCurrentUserAsync();
            if (user.Id == taskList.ApplicationUser.Id)
            {
                var task = taskList.Tasks.Find(x => x.Id == taskId);
                task.Status = false;
                taskList.Completed--;
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("TaskList", "TaskList", new { taskListID });
        }

    }
}
