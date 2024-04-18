using Forums.BusinessLogic.Interfaces;
using Forums.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forums.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly ISession _session;
        public ProfileController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetAuthBL();
        }
        //Logging out of session
        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();

            DeleteCookies();

            return RedirectToAction("HomePage", "Home");
        }
        // GET: Profile
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("HomePage", "Home");
            }
            return View();
        }
        //Delete coockie
        private void DeleteCookies()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                HttpCookie myCookie = new HttpCookie("X-KEY");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }
    }
}