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

            var emp = from e in northwind.Employees
                      orderby e.EmployeeID
                      select e.FirstName;

            var shippers = from s in northwind.Shippers
                           orderby s.CompanyName
                           select s.CompanyName;


            ViewBag.Shippers = shippers.ToList();
            ViewBag.Employees = emp.ToList();

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

        public ActionResult CustOrders(int? id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            // Need to pass the customerId to provide this to ajax call later
            ViewBag.custId = id;

            //var orders = from e in northwind.Employees
            //             where id == e.EmployeeID
            //             orderby e.EmployeeID
            //             select e;

            var orders = from e in northwind.Employees
                         where e.EmployeeID == id
                         orderby e.FirstName
                         select e;


            // ToList() call needed as sproc returns IEnumerable<> rather than IQueryable<>
            //return PartialView("_CustOrders", northwind.CustOrdersOrders(id).ToList().ToPagedList(pageNumber, pageSize));
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_CustOrders", orders);
            //}

            return PartialView("_CustOrders", orders.ToList().ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Delete(int id)
        {
            Order order = northwind.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);

        }


        //worked the first time but now it doesnt seem to work
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //return RedirectToAction("Index", "Home");
            Order order = northwind.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            northwind.Orders.Remove(order);
            northwind.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EmployeeOrders(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var orders = from o in northwind.Orders
                         where o.EmployeeID == id
                         orderby o.OrderID
                         select o;

            return View(orders.ToPagedList(pageNumber, pageSize));
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


        public IEnumerable<string> GetAllEmployees()
        {
            return northwind.Employees.Select(e => e.FirstName);
            //return db.Suppliers.Select(e => e.SupplierID);
        } 
    }
}