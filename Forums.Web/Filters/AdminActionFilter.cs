using Forums.BusinessLogic.Interfaces;
using Forums.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forums.Web.Filters
{
    public class AdminActionFilter: ActionFilterAttribute
    {
        private readonly ISession _session;
        public AdminActionFilter() 
        {
            var businessLogic = new BusinessLogic.BussinesLogic();
            _session = businessLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-Key"];
            if(apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if(profile != null && profile.Level != Domain.Enum.UserRole.Admin) 
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "HomePage" }));
                }
            }
        }
    }
}