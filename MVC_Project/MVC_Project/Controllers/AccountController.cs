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

		// GET 方法用於展示「登入」表單
		[HttpGet]
		public IActionResult Login()
		{
			return View("~/Views/Home/Login.cshtml");
		}

		// GET 方法用於展示「註冊」表單
		[HttpGet]
		public IActionResult Register()
		{
			return View("~/Views/Home/Register.cshtml");
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
		public IActionResult Register(string account, string password, string comfirm_password, string nickname, string email)
		{
			if (password != comfirm_password)
			{
				// 密碼和確認密碼不一致
				return View("~/Views/Home/Register.cshtml", new { error = "密碼和確認密碼不一致" });
			}

			var existingUser = _context.Member.FirstOrDefault(m => m.Account == account);
			if (existingUser != null)
			{
				// 使用者已存在
				return View("~/Views/Home/Register.cshtml", new { error = "使用者已存在" });
			}

			var newUser = new Member
			{
				Account = account,
				Password = password, // 實際應用應使用加密存儲
				Nickname = nickname,
				Email = email,
			};

			_context.Member.Add(newUser);
			_context.SaveChanges();

			// 登入用戶
			HttpContext.Session.SetString("UserId", newUser.UserID.ToString());

			return RedirectToAction("Index", "Home"); // 跳轉至主頁
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
