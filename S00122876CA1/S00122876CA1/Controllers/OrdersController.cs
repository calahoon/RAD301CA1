using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S00122876CA1.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        MusicDatabaseDataContext db = new MusicDatabaseDataContext();

        public ActionResult Index(string searchTerm)
        {
            var query = from od in db.OrderDetails
                        where searchTerm == null || od.Order.FirstName.Contains(searchTerm) || od.Order.LastName.Contains(searchTerm) 
                        orderby od.Order.OrderDate
                        select od;

            
            return View(query);
        }

        //
        // GET: /Orders/Details/5

        public ActionResult OrderBySize(string searchWord)
        {
            var OBS = from od in db.OrderDetails
                      where searchWord == null || od.Order.FirstName.Contains(searchWord) || od.Order.LastName.Contains(searchWord) 
                      orderby od.Order.Total descending
                      select od;
                      

            return View("Index",OBS);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
