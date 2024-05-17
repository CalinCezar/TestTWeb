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
    public class UserBL : UserApi, IUser
    {
        public UserMinimal GetUserDataAction(int ID)
        {
            return GetUserData(ID);
        }
        public GeneralResp EditUserDataAction(UserMinimal data, int ID)
        {
            return EditUserData(data, ID);
        }
        public GeneralResp DeleteUserAction(int ID)
        {
            return DeleteUser(ID);
        }
        public GeneralResp UploadPhotoAction(string photo, int ID)
        {
            return UploadPhoto(photo, ID);
        }
    }
}
