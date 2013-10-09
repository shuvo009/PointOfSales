using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class Transaction : BaseModel, IEntity
    {
        private Customers _customer;
        private double _discount;
        private double _dueTotal;
        private DateTime _entityDate;
        private long _id;
        private string _invoiceNumber;
        private bool _isDeleted;
        private bool _isReturn;
        private bool _isSales;
        private double _netTotal;
        private double _paidTotal;
        private string _remark;
        private double _subTotal;
        private Suppliers _supplier;
        private ObservableCollection<TransactionLedger> _transactionLedgers;

        public Transaction()
        {
            TransactionLedgers = new ObservableCollection<TransactionLedger>();
        }

        public bool IsSales
        {
            get { return _isSales; }
            set
            {
                _isSales = value;
                OnPropertyChange(() => IsSales);
            }
        }

        public bool IsReturn
        {
            get { return _isReturn; }
            set
            {
                _isReturn = value;
                OnPropertyChange(() => IsReturn);
            }
        }

        [Required(ErrorMessage = "Please Enter Invoice Number."), MaxLength(30)]
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

        [Required(ErrorMessage = "Please Enter Sub Total.")]
        public double SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                OnPropertyChange(() => SubTotal);
            }
        }

        [Required(ErrorMessage = "Please Enter Net Total.")]
        public double NetTotal
        {
            get { return _netTotal; }
            set
            {
                _netTotal = value;
                OnPropertyChange(() => NetTotal);
            }
        }

        [Required(ErrorMessage = "Please Enter Paid Total.")]
        public double PaidTotal
        {
            get { return _paidTotal; }
            set
            {
                _paidTotal = value;
                OnPropertyChange(() => PaidTotal);
            }
        }

        [Required(ErrorMessage = "Please Enter Due Total.")]
        public double DueTotal
        {
            get { return _dueTotal; }
            set
            {
                _dueTotal = value;
                OnPropertyChange(() => DueTotal);
            }
        }

        [Required(ErrorMessage = "Please Enter Discount.")]
        public double Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                OnPropertyChange(() => Discount);
            }
        }

        [MaxLength(100)]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark == value) return;
                _remark = value;
                OnPropertyChange(() => Remark);
            }
        }

        [ForeignKey("Customer")]
        public Int64? CustomerId { get; set; }

        public virtual Customers Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChange(() => Customer);
            }
        }

        [ForeignKey("Supplier")]
        public Int64? SupplierId { get; set; }

        public virtual Suppliers Supplier
        {
            get { return _supplier; }
            set
            {
                _supplier = value;
                OnPropertyChange(() => Supplier);
            }
        }

        public virtual ObservableCollection<TransactionLedger> TransactionLedgers
        {
            get { return _transactionLedgers; }
            set
            {
                _transactionLedgers = value;
                OnPropertyChange(() => TransactionLedgers);
            }
        }

        [Key]
        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange(() => Id);
            }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                OnPropertyChange(() => IsDeleted);
            }
        }
    }
}