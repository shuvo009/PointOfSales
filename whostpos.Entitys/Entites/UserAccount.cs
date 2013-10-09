using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class UserAccount : BaseModel, IEntity, IDataErrorInfo
    {
        private long _id;
        private string _password;
        private string _username;

        public UserAccount()
        {
            Permissions = new HashSet<Permission>();
        }

        [MaxLength(100)]
        [Required(ErrorMessage = "Please Enter Username")]
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChange(() => Username);
            }
        }

        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(100)]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange(() => Password);
            }
        }

        public virtual ICollection<Permission> Permissions { get; set; }

        [Key]
        public Int64 Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange(() => Id);
            }
        }

        public bool IsDeleted { get; set; }

        #region Data Error Info

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return InputValidation<UserAccount>.Validate(this, columnName); }
        }

        #endregion
    }
}