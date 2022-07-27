using FifthTemplateforfoodordering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace FifthTemplateforfoodordering.Controllers
{
    public class UserController : Controller
    {
        private readonly FoodContext fd;
        public UserController(FoodContext fd)
        {
            this.fd = fd;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult ViewReport()
        //{

        //}
    }

}
