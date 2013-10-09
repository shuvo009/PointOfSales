using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class Customers : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private string _address;
        private string _email;
        private DateTime _entityDate;
        private Int64 _id;
        private string _mobile;
        private string _name;
        private string _note;
        private byte[] _photo;
        private string _telephone;

        #endregion

        public Customers()
        {
            CustomerLedgers = new HashSet<CustomerLedger>();
        }

        public DateTime EntityDate
        {
            get { return _entityDate; }
            set
            {
                if (_entityDate == value) return;
                _entityDate = value;
                OnPropertyChange(() => EntityDate);
            }
        }

        [Required(ErrorMessage = "Please enter Name.")]
        [MaxLength(30)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChange(() => Name);
            }
        }

        [Required(ErrorMessage = "Please Enter Address.")]
        [MaxLength(100)]
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address == value) return;
                _address = value;
                OnPropertyChange(() => Address);
            }
        }

        [Required(ErrorMessage = "Please Enter Mobile Number.")]
        [MaxLength(20)]
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                if (_mobile == value) return;
                _mobile = value;
                OnPropertyChange(() => Mobile);
            }
        }

        [MaxLength(30)]
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (_telephone == value) return;
                _telephone = value;
                OnPropertyChange(() => Telephone);
            }
        }

        [MaxLength(30)]
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note == value) return;
                _note = value;
                OnPropertyChange(() => Note);
            }
        }

        [MaxLength(30)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChange(() => Email);
            }
        }

        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                if (_photo == value) return;
                _photo = value;
                OnPropertyChange(() => Photo);
            }
        }

        public Int64 CustomerAccountId { get; set; }

        [ForeignKey("CustomerAccountId")]
        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual ICollection<CustomerLedger> CustomerLedgers { get; set; }

        #region Data Error Info

        public string Error
        {
            get
            {
                string errorMessagess = string.Empty;
                PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (
                    PropertyInfo p in
                        properties.Where(p => p.CanWrite && p.CanRead).Where(p => !String.IsNullOrEmpty(this[p.Name])))
                {
                    errorMessagess = "Error";
                }
                return errorMessagess;
            }
        }

        public string this[string columnName]
        {
            get { return InputValidation<Customers>.Validate(this, columnName); }
        }

        #endregion

        [Key]
        public Int64 Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChange(() => Id);
            }
        }

        public bool IsDeleted { get; set; }
    }
}