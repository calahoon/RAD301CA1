using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S00122876CA1.Controllers
{
    public class HomeController : Controller
    {

        MusicDatabaseDataContext db = new MusicDatabaseDataContext();

        public ActionResult Index(string searchTerm)
        {

            //var model = from r in db.OrderDetails
            //            orderby r.AlbumId
            //            select r;

            //var allOrders = db.OrderDetails
            //    .Where(ord => searchTerm == null || ord.Order.FirstName.Contains(searchTerm))
            //    .OrderBy(o => o.Order.FirstName);

            var allOrders = from o in db.OrderDetails
                           where searchTerm == null || o.Order.FirstName.Contains(searchTerm)
                           orderby o.Order.FirstName
                           select o;

            
            return View(allOrders);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Search(string searchTerm)
        {
            var name = from n in db.OrderDetails
                       where n.Order.FirstName.Contains(searchTerm)
                       select n;


            return View("Index", name);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
