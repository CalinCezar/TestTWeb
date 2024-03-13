using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.Interfaces
{
    public interface IAuth
    {
        GeneralResp UserPassCheckAction(ULoginData data);
    }
}

// interfata - contract care specifica ce trebuie sa faca entitatea.