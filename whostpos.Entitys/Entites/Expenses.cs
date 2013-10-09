using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class Expenses : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private double _amount;
        private DateTime _entityDate;
        private Int64 _id;
        private string _particulars;
        private string _voucherNumber;

        #endregion

        [MaxLength(30)]
        public string VoucherNumber
        {
            get { return _voucherNumber; }
            set
            {
                if (_voucherNumber == value) return;
                _voucherNumber = value;
                OnPropertyChange(() => VoucherNumber);
            }
        }

        [Required]
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

        [Required(ErrorMessage = "Please enter Particulars."), MaxLength(500)]
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

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChange(() => Amount);
            }
        }

        #region Data Error Info

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return InputValidation<Expenses>.Validate(this, columnName); }
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