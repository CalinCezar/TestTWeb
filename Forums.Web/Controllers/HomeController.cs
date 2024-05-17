using Forums.Web.Extension;
using Forums.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forums.Web.Controllers
{
    public class HomeController : BaseController
    {
        [AdminActionFilter]
        [AuthorisedActionFilter]
        public ActionResult HomePage()
        {
            SessionStatus();
            return View();
        }
    }
}