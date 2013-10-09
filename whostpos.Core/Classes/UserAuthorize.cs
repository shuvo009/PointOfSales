using System;
using System.Collections.ObjectModel;
using System.Linq;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Domain.Context;
using whostpos.Entitys.Entites;

namespace whostpos.Core.Classes
{
    public sealed class UserAuthorize : IUserAuthorize
    {
        private static UserAuthorize _userAuthorize;

        public ObservableCollection<Permission> Permissions { get; set; }
        public string Username { get; set; }

        private UserAuthorize()
        {

        }

        public static UserAuthorize Authorize
        {
            get
            {
                return _userAuthorize ?? (_userAuthorize = new UserAuthorize());
            }
        }


        public bool UserLogin(string username, string password)
        {
            using (var wposContext = new WposContext())
            {
                var isAuthoriz = wposContext.UserAccountRepository.IsAuthorize(username, MiraculousMethods.Sha256Encrypt(password));
                if (isAuthoriz)
                {
                    Username = username;
                    Permissions = wposContext.UserAccountRepository.GetUserPermissions(username);
                    AddDefultPermission();
                }
                return isAuthoriz;
            }
        }

        public void UserLogout()
        {
            Username = string.Empty;
            Permissions =  new ObservableCollection<Permission>();
        }

        public bool IsAccessable(string itemName)
        {
            if (String.IsNullOrEmpty(Username)) return false;
            if (Username.ToLower().Equals("admin")) return true;
            return Permissions.FirstOrDefault(x => x.PermissionType.Equals(itemName) && x.IsAccessable) != null;
        }

        private void AddDefultPermission()
        {
            Permissions.Add(new Permission { IsAccessable = true, PermissionType = "Home" });
        }
    }
}
