using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class AccountController : Controller
    {
        
            private readonly ProjectXContext _context;

            public AccountController(ProjectXContext context)
            {
                _context = context;
            }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var member = _context.Member.FirstOrDefault(m => m.Account == username && m.Password == password);
            if (member != null)
            {
                HttpContext.Session.SetString("UserId", member.UserID.ToString()); // 儲存使用者ID到 session
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "帳號或密碼錯誤";
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Register(Member member)
        {
            if (ModelState.IsValid)
            {
                var isExist = _context.Member.Any(m => m.Account == member.Account);
                if (!isExist)
                {
                    _context.Member.Add(member);
                    _context.SaveChanges();
                    return RedirectToAction("LoginView"); // 
                }
                else
                {
                    TempData["ErrorMessage"] = "此帳號已存在";
                }
            }
            return RedirectToAction("RegisterView"); //
        }


        ////拿來看一下有沒有抓到，現在UserID是多少
        public IActionResult CheckSession()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                return Content($"Logged in user ID: {userId}");
            }
            else
            {
                return Content("No user is logged in.");
            }
        }

        ////這邊是登出要指向的
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Home");
        }

    }
}
