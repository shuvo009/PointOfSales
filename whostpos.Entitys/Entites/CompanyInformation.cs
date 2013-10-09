using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class CompanyInformation : BaseModel, IEntity, IDataErrorInfo
    {
        [Required(ErrorMessage = "Please Enter Company Name.")]
        [MaxLength(30)]
        public string CompaneyName
        {
            get { return _companeyName; }
            set
            {
                if (_companeyName == value) return;
                _companeyName = value;
                OnPropertyChange(() => CompaneyName);
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

        [Required(ErrorMessage = "Please Enter Contract Details.")]
        [MaxLength(100)]
        public string ContractDetails
        {
            get { return _contractDetails; }
            set
            {
                if (_contractDetails == value) return;
                _contractDetails = value;
                OnPropertyChange(() => ContractDetails);
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

        [MaxLength(50)]
        public string Web
        {
            get { return _web; }
            set
            {
                if (_web == value) return;
                _web = value;
                OnPropertyChange(() => Web);
            }
        }

        [Required(ErrorMessage = "Please Select Company Logo.")]
        public byte[] Logo
        {
            get { return _logo; }
            set
            {
                if (_logo == value) return;
                _logo = value;
                OnPropertyChange(() => Logo);
            }
        }

        [Required(ErrorMessage = "Please Enter Contract Number")]
        public string ContractNumber
        {
            get { return _contractNumber; }
            set
            {
                _contractNumber = value;
                OnPropertyChange(() => ContractNumber);
            }
        }

        #region Private Variables

        private string _address;
        private string _companeyName;
        private string _contractDetails;
        private string _email;
        private Int64 _id;
        private byte[] _logo;
        private string _web;
        private string _contractNumber;

        #endregion

        #region Data Error Information

        public string Error
        {
            get
            {
                var errorMessagess = string.Empty;
                var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var p in properties.Where(p => p.CanWrite && p.CanRead).Where(p => !String.IsNullOrEmpty(this[p.Name])))
                {
                    errorMessagess = "Error";
                }
                return errorMessagess;
            }
        }

        public string this[string columnName]
        {
            get { return InputValidation<CompanyInformation>.Validate(this, columnName); }
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
                OnPropertyChange("Id");
            }
        }

        public bool IsDeleted { get; set; }
    }
}