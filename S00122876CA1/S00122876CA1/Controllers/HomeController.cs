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

        public ActionResult Index()
        {

            var model = from r in db.OrderDetails
                        orderby r.AlbumId
                        select r;


            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
