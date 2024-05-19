using Forums.BusinessLogic.DBModel;
using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using Forums.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using System.Runtime.Remoting.Messaging;
using Forums.Helpers;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using System.Net;
using System.Web.UI;

namespace Forums.BusinessLogic.Core
{
    public class UserApi
    {

        internal GeneralResp UserAuthLogic(ULoginData data)
        {
            //SQL connect, select, check data, logic on data
            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => (u.Username == data.Credential || u.Email == data.Credential) && u.Password == pass);
                }

                if (result == null)
                {
                    return new GeneralResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIP = data.LoginIP;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new GeneralResp { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new GeneralResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIP = data.LoginIP;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new GeneralResp { Status = true };
            }
        }
        internal GeneralResp RegisterUserAction(URegisterData data)
        {
            var newUser = new UDbTable()
            {
                Username = data.Credential,
                Password = LoginHelper.HashGen(data.Password),
                Email = data.Email,
                InfoBlog = data.InfoBlog,
                LastLogin = data.LoginDateTime,
                LasIP = data.LoginIp,
                Level = UserRole.User,
                Profession = String.Empty,
                PhoneNumber = String.Empty,
                Photo = String.Empty,
                Fullname = String.Empty
            };

            // CONNECT WITH DB
            
            using (var db = new UserContext())
            {
                if (db.Users.Any(u => u.Email == data.Email))
                {
                    return new GeneralResp { Status = false, StatusMsg = "Email already exists" };
                }
                else
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
            }
            return new GeneralResp {Status = true};
        }
        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
        internal UserMinimal GetUserData(int ID)
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Id == ID);

                if (result == null)
                {
                    return null;
                }

                UserMinimal userMinimal = new UserMinimal
                {
                    Id = result.Id,
                    Username = result.Username,
                    Email = result.Email,
                    LasIp = result.LasIP,
                    LastLogin = result.LastLogin,
                    Photo = result.Photo,
                    Level = result.Level,
                    InfoBlog = result.InfoBlog,
                    Profession = result.Profession,
                    PhoneNumber = result.PhoneNumber,
                    Fullname = result.Fullname
                };

                return userMinimal;
            }
        }
        internal GeneralResp UploadPhoto(string  photo, int ID) 
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Id == ID);

                if (result == null)
                {
                    return new GeneralResp { Status = false };
                }
                if (result.Photo == photo) return new GeneralResp { Status = false };
                else
                {
                    result.Photo = photo;
                    db.SaveChanges();
                    return new GeneralResp { Status = true };

                }
            }
        }
        internal GeneralResp EditUserData(UserMinimal data, int ID)
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Id == ID);

                if (result == null)
                {
                    return new GeneralResp { Status = false };
                }

                if(result.Fullname ==  data.Fullname &&
                   result.Email == data.Email &&
                   result.InfoBlog == data.InfoBlog &&
                   result.PhoneNumber == data.PhoneNumber &&
                   result.Profession == data.Profession) 
                {
                    return new GeneralResp { Status = false };
                }
                else
                {
                    result.Fullname = data.Fullname;
                    result.Email = data.Email;
                    result.InfoBlog = data.InfoBlog;
                    result.PhoneNumber = data.PhoneNumber;
                    result.Profession = data.Profession;
                    db.SaveChanges();
                    return new GeneralResp { Status = true };
                }
            }
        }
        internal GeneralResp DeleteUser(int ID)
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Id == ID);

                if (result == null)
                {
                    return new GeneralResp { Status = false };
                }
                    db.Users.Remove(result);
                    db.SaveChanges(); 
                    return new GeneralResp { Status = true };
            }
        }
        internal GeneralResp SendEmail(string email, string name, string subject, string body)
        {
            return new GeneralResp { Status = EmailService.SendEmailToUser(email, name, subject, body)}; 
        }
        internal GeneralResp ResetPassword(string email, string password)
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Email == email);

                if (result == null)
                {
                    return new GeneralResp { Status = false };
                }
                result.Password = LoginHelper.HashGen(password);
                db.SaveChanges();
                return new GeneralResp { Status = true };
                
            }
        }
        internal GeneralResp ExistingEmail(string email)
        {
            using (var db = new UserContext())
            {
                var result = db.Users.FirstOrDefault(e => e.Email == email);

                if (result == null)
                {
                    return new GeneralResp { Status = false };
                }
                else return new GeneralResp { Status = true };

            }
        }
    }
}
