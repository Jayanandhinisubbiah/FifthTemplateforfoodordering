﻿using FifthTemplateforfoodordering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FifthTemplateforfoodordering.Controllers
{
    public class CartController : Controller
    {
        private readonly FoodContext fd;
        private readonly ISession session;

        public CartController(FoodContext fd, IHttpContextAccessor httpContextAccessor)
        {
            this.fd = fd;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult MyCart(int? FoodId)
        //{
        //    var result = (from i in fd.Foods
        //                  where i.FoodId == FoodId
        //                  select i.FoodId).SingleOrDefault();
        //    HttpContext.Session.SetString("FoodId", "result");
            
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult MyCart(Cart C)
        //{
        //    fd.Cart.Add(C);
        //    fd.SaveChanges();
        //    return RedirectToAction("Cart");
        //}
        //public IActionResult Cart()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Cart([Bind("UserId,FoodId,Qnt")] Cart C)
        //{
        //    OrderMaster orderMaster = new OrderMaster();
        //    orderMaster.UserId = C.UserId;
        //    OrderDetails orderDetails = new OrderDetails();
        //    orderDetails.FoodId = C.FoodId;
        //    orderDetails.Qnt = C.Qnt;
        //    fd.SaveChanges();
        //    fd.Cart.RemoveRange(fd.Cart.ToList());
        //    fd.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public IActionResult AddtoCart(int? FoodId)
        {
            var P = (from i in fd.Foods
                     where i.FoodId == FoodId
                     select i).SingleOrDefault(); 
            string UserId = HttpContext.Session.GetString("UserId");
            return View(P);
        }
        [HttpPost]
        public IActionResult AddtoCart(int Qnt,int? FoodId)
        {
            string UserId= HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            Cart C = new Cart();
            var F= fd.Foods.FirstOrDefault(i => i.FoodId == FoodId);
            C.UserId = b;
            C.FoodId = F.FoodId;
            C.Qnt = Qnt;
            fd.Add(C);
            fd.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            string UserId=HttpContext.Session.GetString("UserId");
            int b=int.Parse(UserId);
            List<Cart> result=(from i in fd.Cart.Include(x=>x.Food)
                               where i.UserId == b
                               select i).ToList();
            return View(result);
        }
        [HttpPost]
        [ActionName("ViewCart")]
        public IActionResult ViewCartDetails()
        {
            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            var c = (from i in fd.Cart
                     where i.UserId == b
                     select i.FoodId).SingleOrDefault();
            var F= fd.Foods.FirstOrDefault(i => i.FoodId == c);
            List<Cart> list = (from i in fd.Cart
                               where i.UserId == b
                               select i).ToList();
            OrderMaster orderMaster = new OrderMaster();
            orderMaster.UserId = int.Parse(UserId);
            fd.Add(orderMaster);
            fd.SaveChanges();
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            foreach (var item in list)
            {
                OrderDetails od = new OrderDetails();
                od.OrderId = orderMaster.OrderId;
                od.FoodId = item.FoodId;
                od.Qnt = item.Qnt;
                od.Price = F.price;
                od.TotalPrice = od.Qnt * od.Price;
                //orderDetails.Add(od); //S
                fd.OrderDetails.Add(od);
                fd.SaveChanges();
                
            }
            //fd.Add(orderDetails);
            orderDetails.AddRange(fd.OrderDetails);
            orderMaster.TotalPrice = orderDetails.Sum(i=>i.TotalPrice);
            fd.SaveChanges();
            
            return RedirectToAction("Emptylist");
         }
        public IActionResult Delete(int? CartId)
        {
            if (CartId == null)
            {
                return NotFound();
            }

            var val = fd.Cart
                .FirstOrDefault(m => m.CartId == CartId);
            if (val == null)
            {
                return NotFound();
            }

            return View(val);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int CartId)
        {
            var val =  fd.Cart.Find(CartId);
            fd.Cart.Remove(val);
            fd.SaveChanges();
            return RedirectToAction("Viewcart");
        }
        public IActionResult Emptylist()
        {
            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);

            List<Cart> list = (from i in fd.Cart
                               where i.UserId == b
                               select i).ToList();
            foreach (var item in list)
            {
                var val = fd.Cart.Find(item.CartId);
                fd.Cart.Remove(val);
                fd.SaveChanges();
            }

            return RedirectToAction("OrderDetails");
        }
        public IActionResult OrderDetails()
        {
           
            return View(fd.OrderDetails.Include(x=>x.Food).ToList());
        }
       public IActionResult Buy()
        { //   var result=(from i in fd.OrderMasters

            //    select i.TotalPrice).SingleOrDefault();
            //HttpContext.Session.SetString("TotalPrice", "result");
            string UserId = HttpContext.Session.GetString("UserId");
           int b = int.Parse(UserId);
            var result=fd.OrderMasters.SingleOrDefault(m => m.UserId == b);
           
            return View(result);
        }
        [HttpPost]
        [ActionName("Payment")]
        public IActionResult Buy(int OrderId,string Type)
        {
            //string UserId = HttpContext.Session.GetString("UserId");
            //int b = int.Parse(UserId);
            var result = fd.OrderMasters.SingleOrDefault(m => m.OrderId == OrderId); 
            result.Type = Type;
            fd.SaveChanges();
            if (result.Type == "Online")
            {

                return RedirectToAction("Online", new { OrderId = OrderId });
            }
            else
            {
                return RedirectToAction("Offline", new { OrderId = OrderId });
            }
        }
        public IActionResult Online(int OrderId)
        {  
            var result = fd.OrderMasters.SingleOrDefault(m => m.OrderId == OrderId);
            return View(result);
        }
        [HttpPost]
        [ActionName("OnlineType")]
        public IActionResult Online(int OrderId,OrderMaster O)
        {
           
            var result = fd.OrderMasters.SingleOrDefault(m => m.OrderId == OrderId);
            result.BankName = O.BankName;
            result.CardNo = O.CardNo;
            result.CCV=O.CCV;
            fd.SaveChanges();
            return RedirectToAction("ThankYou");
        }
        public IActionResult Offline(int OrderId)
        {
            var result = fd.OrderMasters.SingleOrDefault(m => m.OrderId == OrderId);
            return View(result);
        }
        [HttpPost]
        [ActionName("OfflineType")]
        public IActionResult Offline(int OrderId, OrderMaster O)
        {

            var result = fd.OrderMasters.SingleOrDefault(m => m.OrderId == OrderId);
            result = O;
            fd.SaveChanges();
            return RedirectToAction("ThankYou");
        }
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
