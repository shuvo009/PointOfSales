using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class SupplierAccount : BaseModel, IEntity
    {
        #region Private variable

        private double _balance;
        private double _credit;
        private double _debit;
        private Int64 _id;

        #endregion

        public double Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChange(() => Credit);
                CalculateBalance();
            }
        }

        public double Debit
        {
            get { return _debit; }
            set
            {
                _debit = value;
                OnPropertyChange(() => Debit);
                CalculateBalance();
            }
        }

        [NotMapped]
        public double Balance
        {
            get { return _balance; }
            set
            {
                _balance = Debit - Credit;
                _balance = value;
                OnPropertyChange(() => Balance);
            }
        }

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

        private void CalculateBalance()
        {
            Balance = Credit - Debit;
        }
    }
}