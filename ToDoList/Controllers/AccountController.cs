using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ToDoList.DAL;
using ToDoList.Models;
using System.Text.RegularExpressions;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            // basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter username and password";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index", "TaskItem");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // GET: Account/Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Account/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(string username, string password)
        {
            // USERNAME VALIDATION
            if (string.IsNullOrEmpty(username) ||
                username.Length > 20 ||
                username.Contains(" ") ||
                !Regex.IsMatch(username, "^[A-Za-z0-9]+$"))
            {
                ViewBag.Error = "Username must be max 20 characters, contain only letters and numbers, and no spaces";
                return View();
            }

            // PASSWORD VALIDATION
            if (string.IsNullOrEmpty(password) ||
                password.Length < 8 ||
                password.Contains(" ") ||
                !Regex.IsMatch(password, "[A-Z]") ||
                !Regex.IsMatch(password, "[0-9]") ||
                !Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                ViewBag.Error = "Password must be at least 8 characters and include 1 uppercase letter, 1 number, 1 special character, and no spaces";
                return View();
            }

            // CHECK DUPLICATE USER 
            var existingUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (existingUser != null)
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            // SAVE USER
            var user = new UserModel
            {
                Username = username,
                Password = password
            };

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}