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
    public class Suppliers : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private string _address;
        private string _contractPerson;
        private string _email;
        private DateTime _entityDate;
        private Int64 _id;
        private string _mobile;
        private string _name;
        private string _telephone;
        private string _webSite;

        #endregion

        public Suppliers()
        {
            Productses = new HashSet<Products>();
            SuppliersLedgers = new HashSet<SuppliersLedger>();
        }

        [Required(ErrorMessage = "Please enter Name."), MaxLength(30)]
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

        [Required(ErrorMessage = "Please enter Address."), MaxLength(100)]
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

        [Required(ErrorMessage = "Please enter Mobile Number."), MaxLength(30)]
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

        [MaxLength(30)]
        public string WebSite
        {
            get { return _webSite; }
            set
            {
                if (_webSite == value) return;
                _webSite = value;
                OnPropertyChange(() => WebSite);
            }
        }

        [Required(ErrorMessage = "Please Enter Date.")]
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

        [Required(ErrorMessage = "Please enter Contract Person."), MaxLength(30)]
        public String ContractPerson
        {
            get { return _contractPerson; }
            set
            {
                if (_contractPerson == value) return;
                _contractPerson = value;
                OnPropertyChange(() => ContractPerson);
            }
        }

        public Int64 SupplierAccountId { get; set; }

        [ForeignKey("SupplierAccountId")]
        public virtual SupplierAccount SupplierAccount { get; set; }

        public virtual ICollection<SuppliersLedger> SuppliersLedgers { get; set; }
        public virtual ICollection<Products> Productses { get; set; }

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
            get { return InputValidation<Suppliers>.Validate(this, columnName); }
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