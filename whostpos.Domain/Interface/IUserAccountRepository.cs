using System.Collections.ObjectModel;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface IUserAccountRepository : IBaseRepository<UserAccount>
    {
        bool IsAuthorize(string username, string password);
        ObservableCollection<Permission> GetUserPermissions(string username);
        bool IsNew(string username);
        ObservableCollection<UserAccount> GetUserAccounts();
        bool ChangePassword(string username, string currentpassword, string newPassword);
    }
}
