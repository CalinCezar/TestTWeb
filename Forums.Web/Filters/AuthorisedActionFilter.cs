using Forums.BusinessLogic.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace Forums.Web.Filters
{
    public class AuthorisedActionFilter: ActionFilterAttribute
    {
        private readonly ISession _session;
        public AuthorisedActionFilter()
        {
            var businessLogic = new BusinessLogic.BussinesLogic();
            _session = businessLogic.GetAuthBL();
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
             if((string)HttpContext.Current.Session["LoginStatus"] == "login")
             {
                filterContext.Controller.ViewBag.IsAuthenticated = true;
            }
             else
            {
                filterContext.Controller.ViewBag.IsAuthenticated = false;
            }
        }
    }
}