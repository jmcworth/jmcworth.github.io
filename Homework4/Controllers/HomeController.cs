using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework4.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult RGBColor() {
            ViewBag.Red = Request.QueryString["Red"];
            ViewBag.Green = Request.QueryString["Green"];
            ViewBag.Blue = Request.QueryString["Blue"];
            return View();
        }
    }
}