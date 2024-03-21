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

