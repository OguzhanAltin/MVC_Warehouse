using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Warehouse.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult AdminHomeIndex()
        {
            return View();
        }
    }
}