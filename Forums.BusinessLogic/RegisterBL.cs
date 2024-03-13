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
    public class RegisterBL : UserApi, IRegister
    {
        public GeneralResp UserRegisterCheckAction(URegisterData data)
        {
            return UserRegisterLogic(data);
        }
    }
}
