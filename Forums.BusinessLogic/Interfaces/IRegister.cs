using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.Interfaces
{
    public interface IRegister
    {
        GeneralResp UserRegisterCheckAction(URegisterData data);
    }
}
