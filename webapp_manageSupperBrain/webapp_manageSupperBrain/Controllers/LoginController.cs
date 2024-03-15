using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webapp_manageSupperBrain.Models;

namespace webapp_manageSupperBrain.Controllers
{
    public class LoginController : Controller
    {
       
        ModelsWebDataContext db = new ModelsWebDataContext();
        public ActionResult Login() { return View(); }  
        // GET: Login
        [HttpPost]
       public ActionResult Loginaction(string email ,string password)
        {
           

            UserAccount account = db.UserAccounts.Where(x=>x.emails == email && x.passwords == password).FirstOrDefault();
            if (account != null)
            {
                // Tạo một cookie mới
                HttpCookie cookie = new HttpCookie("MyCookie");

                // Thiết lập giá trị cho cookie
                cookie.Value = account.idUserAccount.ToString();

                // Thiết lập các thuộc tính khác của cookie nếu cần thiết, ví dụ như hạn sử dụng
                cookie.Expires = DateTime.Now.AddDays(1); // Cookie sẽ hết hạn sau 1 ngày

                // Thêm cookie vào response
                Response.Cookies.Add(cookie);
                return RedirectToAction("Home", "Home");
            } else
            {
                return RedirectToAction("Login", "Login");
            }
          

        }

    }
}