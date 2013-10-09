using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class BankTransactions : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variables

        private string _chequeNumber;
        private DateTime _dateOfTan;
        private double _deposit;
        private Int64 _id;
        private string _particulars;
        private double _withdrawal;
        private BankAccounts _bankAccount;

        #endregion

        [MaxLength(30)]
        public string ChequeNumber
        {
            get { return _chequeNumber; }
            set
            {
                if (_chequeNumber == value) return;
                _chequeNumber = value;
                OnPropertyChange(() => ChequeNumber);
            }
        }

        [Required(ErrorMessage = "Please Select A Date.")]
        public DateTime DateOfTan
        {
            get { return _dateOfTan; }
            set
            {
                if (_dateOfTan == value) return;
                _dateOfTan = value;
                OnPropertyChange(() => DateOfTan);
            }
        }

        [MaxLength(100), Required(ErrorMessage = "Please Enter Particular.")]
        public string Particulars
        {
            get { return _particulars; }
            set
            {
                if (_particulars == value) return;
                _particulars = value;
                OnPropertyChange(() => Particulars);
            }
        }

        public double Deposit
        {
            get { return _deposit; }
            set
            {
                _deposit = value;
                OnPropertyChange(() => Deposit);
            }
        }

        public double Withdrawal
        {
            get { return _withdrawal; }
            set
            {
                _withdrawal = value;
                OnPropertyChange(() => Withdrawal);
            }
        }

        public Int64 BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccounts BankAccount
        {
            get { return _bankAccount; }
            set
            {
                _bankAccount = value;
                OnPropertyChange(() => BankAccount);
            }
        }

        #region Data Error Info

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return InputValidation<BankTransactions>.Validate(this, columnName); }
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