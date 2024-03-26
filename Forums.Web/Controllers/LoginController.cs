using Forums.BusinessLogic.Interfaces;
using Forums.BusinessLogic;
using Forums.Domain.Entities.User;
using Forums.Domain.Entities.Response;
using Forums.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Optimization;

namespace Forums.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetAuthBL();
        }

        // GET: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin uLogin)
        {
            if (ModelState.IsValid)
            {
                ULoginData user = new ULoginData
                {
                    Credential = uLogin.Credential,
                    Password = uLogin.Password,
                    LoginIP = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                GeneralResp resp = _session.UserPassCheckAction(user);

                if (resp.Status)
                {
                    //ADD COOKIE
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", resp.StatusMsg);
                    return View(uLogin);
                }
            }
            return View(uLogin);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}