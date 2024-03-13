using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.Core
{
    public class UserApi
    {
        internal GeneralResp UserAuthLogic(ULoginData data)
        {

            //SQL connect, select, check data, logic on data


            //return new GeneralResp();
            return new GeneralResp { Status = false, StatusMsg = null };
        }
        internal GeneralResp UserRegisterLogic(URegisterData data)
        {
            return new GeneralResp { Status = false, StatusMsg = null };
        }

    }


}
