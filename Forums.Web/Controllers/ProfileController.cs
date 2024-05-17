﻿using Forums.BusinessLogic.Interfaces;
using Forums.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forums.Web.Models;
using Forums.BusinessLogic.DBModel;
using System.Data.Entity;
using Forums.Domain.Entities.User;
using Forums.Web.Extension;
using Forums.Domain.Entities.Response;
using System.IO;
namespace Forums.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly ISession _session;
        private readonly IUser _user;
        public ProfileController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
            _user = bl.GetUserBL();
        }
        //Logging out of session
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            DeleteCookies();

            return RedirectToAction("HomePage", "Home");
        }
        public ActionResult DeleteUser()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var userData = _user.GetUserDataAction(user.Id);
            var oldFilePath = Server.MapPath(userData.Photo);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            GeneralResp resp = _user.DeleteUserAction(user.Id);
            if(resp.Status)
            {
                Session.Clear();
                Session.Abandon();
                DeleteCookies();
                
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult EditProfile(UserData data)
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (ModelState.IsValid)
            {
                UserMinimal dataUser = new UserMinimal
                {
                    Username = data.Username,
                    Fullname = data.Fullname,
                    Email = data.Email,
                    InfoBlog = data.InfoBlog,
                    Profession = data.Profession,
                    PhoneNumber = data.PhoneNumber,
                    
                };

                GeneralResp resp = _user.EditUserDataAction(dataUser, user.Id);

                if (resp.Status)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully!";   
                }
            }
            return RedirectToAction("Index", "Profile");
        }
        // GET: Profile
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            SessionStatus();
            
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("HomePage", "Home");
            }

            var userByID = _user.GetUserDataAction(user.Id);
            var currentUser = new UserData()
            {
                Username = userByID.Username,
                Fullname = userByID.Fullname,
                Email = userByID.Email,
                InfoBlog = userByID.InfoBlog,
                Photo = userByID.Photo,
                PhoneNumber = userByID.PhoneNumber,
                Profession = userByID.Profession
            };

            return View(currentUser);
        }

        public ActionResult UploadPhoto()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            string filePath = String.Empty;
            string fileName = String.Empty;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var userData = _user.GetUserDataAction(user.Id);
                    var oldFilePath = Server.MapPath(userData.Photo);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                    file.SaveAs(filePath);
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                var userData = _user.GetUserDataAction(user.Id);
                fileName = Path.GetFileName(userData.Photo);
            }
            var uData = _user.GetUserDataAction(user.Id);
            uData.Photo = "~/Uploads/" + fileName;
            GeneralResp resp = _user.UploadPhotoAction(uData.Photo, user.Id);

            var photoUrl = Url.Content("~/Uploads/" + fileName);
            return Json(new { status = resp.Status, photoUrl = photoUrl });
        }


        //Delete cookie
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
