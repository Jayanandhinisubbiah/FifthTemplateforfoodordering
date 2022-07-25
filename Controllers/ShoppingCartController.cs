//using FifthTemplateforfoodordering.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;

//namespace FifthTemplateforfoodordering.Controllers
//{
//    public class ShoppingCartController : Controller
//    { // getting Shopping Cart
//        private readonly FoodContext fd;
//        private readonly ISession session;

//        public ShoppingCartController(FoodContext fd, IHttpContextAccessor httpContextAccessor)
//        {
//            this.fd = fd;
//            session = httpContextAccessor.HttpContext.Session;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult OrderNow(int? FoodId )
//        {
//            if (FoodId == null)
//            {
//                return NotFound();
//            }
            
//            if(ViewBag.OrderDetails==null) // if Cart is empty
//            {
//                List<OrderDetails> orders = new List<OrderDetails>();
//                //{
//                //    new OrderDetails(fd.Foods.Find(FoodId), 1);
//                //};
//                orders.Add(new OrderDetails(fd.Foods.Find(FoodId), 1));
//                ViewBag.OrderDetails = orders;
//                //HttpContext.Session.SetString("OrderDetails", "orders");
//            }
//            else
//            {
//                List<OrderDetails> orders= (List<OrderDetails>) ViewBag.OrderDetails; // if cart contains items
//                int check = IsExistingCheck(FoodId); // Getting the Index of exisitng Food
//                if (check == -1) // if that foodId index not found
//                    orders.Add(new OrderDetails(fd.Foods.Find(FoodId), 1)); // Add One into Cart
//                else
//                    orders[check].Qnt++; // else increase the quantity of existing one
//                //orders.Add(new OrderDetails(fd.Foods.Find(FoodId), 1));
//                ViewBag.OrderDetails = orders;
//                //HttpContext.Session.SetString("OrderDetails", "orders");// storing them in viewBag
//            }
//            return View("Index");
//        }
////        [HttpPost]
////        public IActionResult OrderNow(OrderDetails OD)
////        {
////            fd.OrderDetails.Add(OD);
////            fd.SaveChanges()
////;return RedirectToAction("Index", "Foods");
////        }
//        public int IsExistingCheck(int? FoodId)
//        {
//            List<OrderDetails> orders = (List<OrderDetails>) ViewBag.OrderDetails;
//            for(int i=0; i < orders.Count; i++)
//            {
//                if (orders[i].Food.FoodId==FoodId)  return i;
//            }
//            return -1;
//        }
//        public IActionResult Delete(int? FoodId)
//        {
//            if (FoodId == null)
//            {
//                return NotFound();
//            }
//            int check = IsExistingCheck(FoodId);
//            List<OrderDetails> orders = (List<OrderDetails>)ViewBag.OrderDetails;
//            orders.RemoveAt(check);
//            return View("Index");
//        }
//    }
//}
