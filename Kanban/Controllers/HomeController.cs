using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kanban.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //return View();
            return RedirectToAction("Kanban");
        }

        public ActionResult Kanban()
        {
            ViewBag.Title = "Kanban";
            return View();
        }
    }
}
