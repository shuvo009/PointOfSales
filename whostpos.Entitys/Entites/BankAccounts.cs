using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class BankAccounts : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private double _accountBalance;
        private string _accountName;
        private string _bankAccountNumber;
        private string _bankName;
        private Int64 _id;

        #endregion

        public BankAccounts()
        {
            BankTransaction = new HashSet<BankTransactions>();
        }

        [MaxLength(30), Required(ErrorMessage = "Please Enter Account Number")]
        public string BankAccountNumber
        {
            get { return _bankAccountNumber; }
            set
            {
                if (_bankAccountNumber == value) return;
                _bankAccountNumber = value;
                OnPropertyChange(() => BankAccountNumber);
            }
        }

        [MaxLength(30), Required(ErrorMessage = "Please Enter Bank Name")]
        public string BankName
        {
            get { return _bankName; }
            set
            {
                if (_bankName == value) return;
                _bankName = value;
                OnPropertyChange(() => BankName);
            }
        }

        [MaxLength(30), Required(ErrorMessage = "Please Enter Account Name")]
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                if (_accountName == value) return;
                _accountName = value;
                OnPropertyChange(() => AccountName);
            }
        }

        public double AccountBalance
        {
            get { return _accountBalance; }
            set
            {
                _accountBalance = value;
                OnPropertyChange(() => AccountBalance);
            }
        }

        public virtual ICollection<BankTransactions> BankTransaction { get; set; }

        #region Error Data

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
            get { return InputValidation<BankAccounts>.Validate(this, columnName); }
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