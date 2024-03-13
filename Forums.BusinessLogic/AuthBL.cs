using Forums.BusinessLogic.Core;
using Forums.BusinessLogic.Interfaces;
using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic
{
    public class AuthBL : UserApi, IAuth // IAuth - se implementeaza contractul
    {
        public GeneralResp UserPassCheckAction(ULoginData data)
        {
            return UserAuthLogic(data);
        }
    }
}
