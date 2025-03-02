namespace BaseProjectApi.Models
{
    public class InternalModels
    {
    }

    public class ServiceModel
    {
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
    }

    public class RequestModel
    {
        public string Action { get; set; }
        public object Payload { get; set; }
    }

    public class UsersModel
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserToken { get; set; }
        public string Epoc { get; set; }
        public DateTime DateTouched { get; set; }
    }

    public class UsersProfile
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string Usersname { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }

    }

    public class EncryptionClass
    {
        public string epocString { get; set; }
        public byte[] key { get; set; }
        public byte[] iv { get; set; }
    }

    public class DBServiceModel<T>
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
    }

    public class SelectionFilterModel
    {
        public int OffSetNumber { get; set; }
        public int LimitedNumber { get; set; }
        public string FilterSearchName { get; set; }
        public int TotalCount { get; set; }


    }

    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResult
    {
        public UsersProfile Profile { get; set; }
        public string UserToken { get; set; }
    }
}
