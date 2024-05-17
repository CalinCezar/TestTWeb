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
using Microsoft.Ajax.Utilities;
using Forums.Web.Extension;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Core.Objects;

namespace Forums.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult Index()
        {
            return View();
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
                    HttpCookie cookie = _session.GenCookie(uLogin.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ModelState.AddModelError("", resp.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}



 
