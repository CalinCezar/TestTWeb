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

namespace Forums.BusinessLogic.Core
{
    public class UserApi
    {
        
        internal GeneralResp UserAuthLogic(ULoginData data)
        {
            //SQL connect, select, check data, logic on data
            /*            UDbTable user;
                        using (var db = new UserContext())
                        {
                            user = db.Users.FirstOrDefault(u => (u.Username == data.Credential || u.Email == data.Credential) && u.Password == data.Password);
                        }

                        if(user == null) return new GeneralResp { Status = false, StatusMsg="Invalid Credentials"};
                        return new GeneralResp { Status = true };*/
            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => (u.Username == data.Credential || u.Email == data.Credential) && u.Password == data.Password);
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
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == data.Password);
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
                Password = data.Password,
                Email = data.Email,
                InfoBlog = data.InfoBlog,
                LastLogin = data.LoginDateTime,
                LasIP = data.LoginIp,
                Level = UserRole.User
     
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
            Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}
