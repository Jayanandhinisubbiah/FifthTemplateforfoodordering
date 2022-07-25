using FifthTemplateforfoodordering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace FifthTemplateforfoodordering.Controllers
{
    public class LoginController : Controller
    {
        private readonly FoodContext fd;
        private readonly ISession session;
        public LoginController(FoodContext fd, IHttpContextAccessor httpContextAccessor)
        {
            this.fd = fd;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User U)
        {
            fd.Users.Add(U);
            fd.SaveChanges();
            if (U.Role == "Admin")
                return RedirectToAction("Index", "Admin");
            else
                return RedirectToAction("Index", "User");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User U)
        {

            var result = (from i in fd.Users
                          where i.FName == U.FName && i.Password == U.Password && i.Role == U.Role
                          select i).SingleOrDefault();
            HttpContext.Session.SetString("UserId", result.UserId.ToString());
            if (U.Role == "Admin")
            {
               
                return RedirectToAction("Index", "Admin");
            }
            else if (U.Role == "User")
            {
                //HttpContext.Session.SetString("UserId", result.UserId.ToString());
                return RedirectToAction("Index", "Foods");
            }
            else
                return RedirectToAction("Login", "Login");
            
           
        }
    }
}
