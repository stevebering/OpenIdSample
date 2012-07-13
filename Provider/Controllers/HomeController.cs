using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Provider.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            Response.AddHeader(
                "X-XRDS-Location",
                new Uri(Request.Url, Response.ApplyAppPathModifier("~/Home/xrds")).AbsolutePath);

            return View();
        }

        public ActionResult Xrds() {
            return View();
        }
    }
}
