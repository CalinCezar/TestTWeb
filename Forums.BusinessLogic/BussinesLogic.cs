using Forums.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic
{
    public class BussinesLogic
    {
        public IAuth GetAuthBL()
        {
            return new AuthBL();
        }
        public IRegister GetRegisterBL()
        {
            return new RegisterBL();
        }
    }
}

