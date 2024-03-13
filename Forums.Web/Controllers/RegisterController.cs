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

namespace Forums.Web.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IRegister _auth;
        public RegisterController()
        {
            var bl = new BussinesLogic();
            _auth = bl.GetRegisterBL();
        }

        // GET: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserRegister uRegis)
        {
            if (ModelState.IsValid)
            {
                URegisterData user = new URegisterData
                {
                    Credential = uRegis.Credential,
                    Password = uRegis.Password,
                    Email = uRegis.Email,
                    Address = uRegis.Address,
                    RegisterIP = Request.UserHostAddress,
                    RegisterDateTime = DateTime.Now
                };



                GeneralResp resp = _auth.UserRegisterCheckAction(user);

                if (resp.Status)
                {
                    //ADD COOKIE
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", resp.StatusMsg);
                    return View(uRegis);
                }
            }
            return View(uRegis);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}