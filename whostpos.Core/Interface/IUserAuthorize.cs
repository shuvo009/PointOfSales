namespace whostpos.Core.Interface
{
    public interface IUserAuthorize
    {
        bool UserLogin(string username, string password);
        bool IsAccessable(string itemName);
    }
}
