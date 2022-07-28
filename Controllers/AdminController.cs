using FifthTemplateforfoodordering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FifthTemplateforfoodordering.Controllers
{
    public class AdminController : Controller
    {
        private readonly FoodContext fd;
        public AdminController(FoodContext fd)
        {
            this.fd = fd;
        }
       
        public IActionResult Index()
        {
            return View(fd.Users.ToList());
        }
    }
}
