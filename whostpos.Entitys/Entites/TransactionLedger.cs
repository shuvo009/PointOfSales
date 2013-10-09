using System;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class TransactionLedger : BaseModel, IEntity
    {
        private double _amount;
        private long _id;
        private bool _isDeleted;
        private Products _product;
        private int _quantity;
        private double _rate;
        private double _salesRate;

        public double Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChange(() => Rate);
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                OnPropertyChange(() => Quantity);
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

        public double SalesRate
        {
            get { return _salesRate; }
            set
            {
                _salesRate = value;
                OnPropertyChange(() => SalesRate);
            }
        }

        [ForeignKey("Product")]
        public Int64 ProductId { get; set; }

        public virtual Products Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChange(() => Product);
            }
        }

        [ForeignKey("Transaction")]
        public Int64 TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }

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