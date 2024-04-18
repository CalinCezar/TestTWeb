using AutoMapper;
using Forums.Domain.Entities.User;
using Forums.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Cryptography;
namespace Forums.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutoMapper();
        }  
            protected static void InitializeAutoMapper()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<UDbTable, UserMinimal>();
                    cfg.CreateMap<ULoginData, UserLogin>();
                    cfg.CreateMap<URegisterData, UserRegister>();
                });
           
            }
    }
}