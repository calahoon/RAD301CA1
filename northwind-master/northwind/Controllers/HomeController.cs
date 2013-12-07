using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace northwind.Controllers
{
    public class HomeController : Controller
    {
        private nwEntities northwind = new nwEntities();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            northwind.Dispose();
        }

        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var orders = from o in northwind.Orders
                            orderby o.OrderID
                            select o;

           

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Cust", orders.ToPagedList(pageNumber, pageSize));
                //return PartialView("_Cust", northwind.Customers.OrderBy(c => c.CompanyName).ToPagedList(pageNumber, pageSize));
            }
            //return View(northwind.Customers.OrderBy(c => c.CompanyName).ToPagedList(pageNumber, pageSize));
            return View(orders.ToPagedList(pageNumber, pageSize));


            //if (Request.IsAjaxRequest())
            //{
            //    //return PartialView("_Cust", customers.ToPagedList(pageNumber, pageSize));
            //    return PartialView("_Cust", northwind.Orders.OrderBy(c => c.OrderID).ToPagedList(pageNumber, pageSize));
            //}
            //return View(northwind.Orders.OrderBy(c => c.OrderID).ToPagedList(pageNumber, pageSize));
            ////return View(customers.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CustOrders(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            // Need to pass the customerId to provide this to ajax call later
            ViewBag.custId = id;
            // ToList() call needed as sproc returns IEnumerable<> rather than IQueryable<>
            return PartialView("_CustOrders", northwind.CustOrdersOrders(id).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}