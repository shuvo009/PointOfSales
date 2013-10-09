using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;

namespace whostpos.Entitys.Entites
{
    public class Permission : BaseModel
    {
        private long _id;
        private string _permissionType;
        private bool _isAccessable;

        public Int64 Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange(() => Id);
            }
        }

        [MaxLength(100)]
        public string PermissionType
        {
            get { return _permissionType; }
            set
            {
                _permissionType = value;
                OnPropertyChange(() => PermissionType);
            }
        }

        public bool IsAccessable
        {
            get { return _isAccessable; }
            set
            {
                _isAccessable = value;
                OnPropertyChange(() => IsAccessable);
            }
        }

        [ForeignKey("UserAccount")]
        public Int64 UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

    }
}