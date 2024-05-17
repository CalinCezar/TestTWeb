using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserMinimal GetUserDataAction(int ID);
        GeneralResp DeleteUserAction(int ID);
        GeneralResp EditUserDataAction(UserMinimal data, int ID);
        GeneralResp UploadPhotoAction(string  photo, int ID);
    }
}
