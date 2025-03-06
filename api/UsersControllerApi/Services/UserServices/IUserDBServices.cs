using BaseProjectApi.Models;

namespace BaseProjectApi.Services.UserServices
{
    public interface IUserDBServices
    {
        Task<ServiceModel> RegisterUser(UsersModel usrm, UsersProfile usrp);
        Task<ServiceModel> UserLogin(string UserName);
        Task<ServiceModel> GetSingleUser(string UserId);
        Task<ServiceModel> GetAllUsers(SelectionFilterModel filter);
        Task<ServiceModel> UpdateUser(UsersModel usrm);
        Task<ServiceModel> DeleteSingleUser(string userId);
        Task<ServiceModel> DeleteAllUsers();
        Task<ServiceModel> CheckIfUserNameExist(UsersModel usrm);
    }
}
