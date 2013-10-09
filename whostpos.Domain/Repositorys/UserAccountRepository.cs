using System.Collections.ObjectModel;
using System.Linq;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
        }

        public bool IsAuthorize(string username, string password)
        {
            username = username.ToLower();
            return DbSetBasic.FirstOrDefault(x => x.Username.ToLower().Equals(username) && x.Password.Equals(password) && !x.IsDeleted) != null;
        }

        public ObservableCollection<Permission> GetUserPermissions(string username)
        {
            username = username.ToLower();
            var userInfo = DbSetBasic.FirstOrDefault(x => x.Username.ToLower().Equals(username));
            return userInfo != null
                       ? new ObservableCollection<Permission>(userInfo.Permissions)
                       : new ObservableCollection<Permission>();
        }

        public bool IsNew(string username)
        {
            username = username.ToLower();
            return DbSetBasic.FirstOrDefault(x => x.Username.ToLower().Equals(username) && !x.IsDeleted) == null;
        }

        public ObservableCollection<UserAccount> GetUserAccounts()
        {
            return new ObservableCollection<UserAccount>(DbSetBasic.Where(x => !x.IsDeleted && !x.Username.Equals("Admin")).ToList());
        }

        public override void Remove(UserAccount entity)
        {
            using (var whostPostContext = new Dbesm())
            {
                var userInfo = whostPostContext.UserAccounts.FirstOrDefault(x => x.Username.Equals(entity.Username.ToLower()) && !x.IsDeleted);
                if (userInfo != null)
                    entity.Password = userInfo.Password;
            }
            base.Remove(entity);
        }

        public bool ChangePassword(string username, string currentpassword, string newPassword)
        {
            username = username.ToLower();
            var userInfo = DbSetBasic.FirstOrDefault(x => x.Username.ToLower().Equals(username) && !x.IsDeleted);
            if (userInfo == null) return false;
            if (!userInfo.Password.Equals(currentpassword)) return false;
            userInfo.Password = newPassword;
            Update(userInfo);
            return true;
        }
    }
}
