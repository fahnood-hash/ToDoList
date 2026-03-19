using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskItemController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: TaskItem
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = db.TaskItems
                          .Where(t => t.UserId == currentUser.Id)
                          .ToList();

            return View(tasks);
        }

        // GET: TaskItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var taskItemModel = db.TaskItems
                                  .FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (taskItemModel == null)
            {
                return HttpNotFound();
            }

            return View(taskItemModel);
        }

        // GET: TaskItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Status,DueDate")] TaskItemModel taskItemModel)
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                taskItemModel.UserId = currentUser.Id;

                if (string.IsNullOrEmpty(taskItemModel.Status))
                {
                    taskItemModel.Status = "Pending";
                }

                db.TaskItems.Add(taskItemModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskItemModel);
        }

        // GET: TaskItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var taskItemModel = db.TaskItems
                                  .FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (taskItemModel == null)
            {
                return HttpNotFound();
            }

            return View(taskItemModel);
        }

        // POST: TaskItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Status,DueDate")] TaskItemModel taskItemModel)
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingTask = db.TaskItems
                                 .FirstOrDefault(t => t.Id == taskItemModel.Id && t.UserId == currentUser.Id);

            if (existingTask == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                existingTask.Title = taskItemModel.Title;
                existingTask.Status = string.IsNullOrEmpty(taskItemModel.Status) ? "Pending" : taskItemModel.Status;
                existingTask.DueDate = taskItemModel.DueDate;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskItemModel);
        }

        // GET: TaskItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var taskItemModel = db.TaskItems
                                  .FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (taskItemModel == null)
            {
                return HttpNotFound();
            }

            return View(taskItemModel);
        }

        // POST: TaskItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var taskItemModel = db.TaskItems
                                  .FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (taskItemModel == null)
            {
                return HttpNotFound();
            }

            db.TaskItems.Remove(taskItemModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id, string status)
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var task = db.TaskItems.FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (task == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            task.Status = status;
            db.SaveChanges();

            return Json(new { success = true, newStatus = task.Status });
        }

        [HttpPost]
        public JsonResult UpdateDueDate(int id, string dueDate)
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var task = db.TaskItems.FirstOrDefault(t => t.Id == id && t.UserId == currentUser.Id);

            if (task == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            if (string.IsNullOrEmpty(dueDate))
            {
                task.DueDate = null;
            }
            else
            {
                DateTime parsedDate;
                if (!DateTime.TryParse(dueDate, out parsedDate))
                {
                    return Json(new { success = false, message = "Invalid date" });
                }

                task.DueDate = parsedDate;
            }

            db.SaveChanges();

            return Json(new
            {
                success = true,
                dueDate = task.DueDate.HasValue
                    ? task.DueDate.Value.ToString("yyyy-MM-dd")
                    : ""
            });
        }
        //Dashboard JA
        public ActionResult Dashboard()
        {
            var username = User.Identity.Name;
            var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = db.TaskItems.Where(t => t.UserId == currentUser.Id).ToList();

            int totalTasks = tasks.Count;
            int completedTasks = tasks.Count(t => t.Status == "Complete");
            int pendingTasks = tasks.Count(t => t.Status == "Pending");
            int inProgressTasks = tasks.Count(t => t.Status == "In Progress");
            
            double completedPercentage = 0;

            if (totalTasks > 0)
            {
                completedPercentage = (double)completedTasks / totalTasks * 100;
            }

            var model = new DashboardModel
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                InProgressTasks = inProgressTasks,
                CompletedPercentage = completedPercentage
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult UpdateCategory(int id, string category)
        {
            var task = db.TaskItems.Find(id);

            if (task == null)
            {
                return Json(new { success = false, message = "Task not found." });
            }

            task.Category = category;
            db.SaveChanges();

            return Json(new { success = true });
        }
    }
}