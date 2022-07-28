﻿using FifthTemplateforfoodordering.Models;
using FifthTemplateforfoodordering.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            return View(fd.Users.ToList());
        }
        public IActionResult ViewReport(int UserId)
        {
            var result = (from i in fd.OrderMasters
                          where i.UserId == UserId
                          select i).SingleOrDefault();
            if(result!= null)
            {
                var res = (from i in fd.OrderDetails
                           where i.OrderId == result.OrderId
                           select i).ToList();
                return View(res);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }

}
