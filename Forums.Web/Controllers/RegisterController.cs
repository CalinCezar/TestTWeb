﻿using Forums.BusinessLogic.Interfaces;
using Forums.BusinessLogic;
using Forums.Domain.Entities.User;
using Forums.Domain.Entities.Response;
using Forums.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Security.Policy;

namespace Forums.Web.Controllers
{
    public class RegisterController : BaseController
    {

        private readonly ISession _session;
        public RegisterController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserRegister uRegis)
        {
            if (ModelState.IsValid)
            {
                if ((Session["VerificationCode"] as int?) != uRegis.VerificationCode)
                {
                    ModelState.AddModelError("", "Invalid verification code!");
                    return View(uRegis);
                }
                URegisterData user = new URegisterData
                {
                    Credential = uRegis.Credential,
                    Password = uRegis.Password,
                    Email = uRegis.Email,
                    InfoBlog = uRegis.InfoBlog,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                GeneralResp resp = _session.RegisterNewUserAction(user);

                if (resp.Status)
                {
                    //ADD COOKIE
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", resp.StatusMsg);
                    return View(uRegis);
                }
            }
            return View(uRegis);
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCode(string email)
        {
            Random random = new Random();
            int verificationCode = random.Next(123456, 1000000);
 
            GeneralResp resp = _session.SendEmailToUserAction(email, "Name", "Verification code for account registration", "Code: " + verificationCode);
            if(resp.Status) 
            {
                Session["VerificationCode"] = verificationCode;
            }
            return Json(new { success = resp.Status });
        }
    }
}