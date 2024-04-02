using System.Web;
using System.Net;
using Forums.BusinessLogic.Core;
using Forums.BusinessLogic.Interfaces;
using Forums.Domain.Entities.User;

namespace Forums.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetAuthBL()
        {
            return new SessionBL();
        }
        public ISession GetRegisterBL()
        {
            return new SessionBL();
        }
    }
}
