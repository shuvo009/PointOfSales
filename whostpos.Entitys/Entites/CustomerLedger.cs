using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class CustomerLedger : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private double _credit;
        private double _debit;
        private DateTime _entityDate;
        private Int64 _id;
        private string _invoiceNumber;
        private string _particulars;

        #endregion

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

        [MaxLength(30)]
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                if (_invoiceNumber == value) return;
                _invoiceNumber = value;
                OnPropertyChange(() => InvoiceNumber);
            }
        }

        [MaxLength(30), Required(ErrorMessage = "Please enter Particulars.")]
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

        public double Debit
        {
            get { return _debit; }
            set
            {
                _debit = value;
                OnPropertyChange(() => Debit);
            }
        }

        public double Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChange(() => Credit);
            }
        }

        [ForeignKey("Customers")]
        public Int64 CustomersId { get; set; }
        public virtual Customers Customers { get; set; }

        #region Data Error Info

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return InputValidation<CustomerLedger>.Validate(this, columnName); }
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