
using BaseProjectApi.Models;

namespace BaseProjectApi.Services.UserService
{
    public interface IDBUsersChecks
    {
        Task<ServiceModel> CheckIfUserIdExist(string userId);
        Task<ServiceModel> CheckIfUserNameExist(string UserName);
        Task<ServiceModel> CheckUsernameOnUpdateDuplicate(UsersModel usrm);
        Task<ServiceModel> GetUserTotalCount();
    }
}
