using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forums.BusinessLogic.Interfaces;
using Forums.BusinessLogic;
using Forums.Domain.Entities.User;
using Forums.Domain.Entities.Response;
using Forums.Web.Models;
using Forums.Web.Extension;
using Forums.Web.Filters;
namespace Forums.Web.Controllers
{
    public class ResetPasswordController : BaseController
    {
        private readonly ISession _session;
        public ResetPasswordController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserRegister uRegis)
        {
            if (!string.IsNullOrEmpty(uRegis.Email))
            {
                GeneralResp resp = _session.ExistingEmailInDB(uRegis.Email);
                if (!resp.Status) 
                {
                    return View(uRegis);
                }

                Session.Remove("ResetToken");
                Session.Remove("ResetTokenExpiration");
                Session.Remove("Email");

                string token = Guid.NewGuid().ToString();
                Session["ResetToken"] = token;
                Session["ResetTokenExpiration"] = DateTime.Now.AddMinutes(5); 
                Session["Email"] = uRegis.Email;

                string resetLink = Url.Action("Reset", "ResetPassword", new { token = token, email = uRegis.Email }, protocol: Request.Url.Scheme);
                var response = _session.SendEmailToUserAction(uRegis.Email, "Name", "Reset your password", $"Please reset your password by clicking on this link: {resetLink}");

                return Json(new { success = response.Status });
            }
            return View(uRegis);
        }


        public ActionResult Reset(string token, string email)
        {
            var resetTokenExpiration = Session["ResetTokenExpiration"] as DateTime?;
            if ((Session["ResetToken"] as string) == token && email == Session["Email"] as string && resetTokenExpiration.HasValue && resetTokenExpiration.Value > DateTime.Now)
            {
                Session["Email"] = email;
                return View();
            }
            return RedirectToAction("Index", "ResetPassword");
        }
        [HttpPost]
        public ActionResult ResettingProcess(UserRegister data)
        {
            if(string.IsNullOrEmpty(data.Password) ||  string.IsNullOrEmpty(Session["Email"].ToString())) 
            {
                return RedirectToAction("Index", "ResetPassword");
            }
            GeneralResp resp = _session.ResetPasswordAction(Session["Email"].ToString(), data.Password);
            Session.Remove("ResetToken");
            Session.Remove("ResetTokenExpiration");
            Session.Remove("Email");
            return Json(new { success = resp.Status }); 
        }
    }
}