using BaseProjectApi.Models;

namespace BaseProjectApi.Services.UserServices
{
    public interface IUserDBServices
    {
        Task<ServiceModel> RegisterUser(UsersModel usrm, UsersProfile usrp);
        Task<ServiceModel> UserLogin(bool UserName);
        Task<ServiceModel> GetSingleUser(string UserId);
        Task<ServiceModel> GetAllUsers();
        Task<ServiceModel> UpdateUser(UsersModel usrm);
        Task<ServiceModel> DeleteSingleUser();
        Task<ServiceModel> DeleteAllUsers();
    }
}
