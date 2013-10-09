using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class CustomerAccount : BaseModel, IEntity
    {
        #region Private Variable

        private double _balance;
        private double _credit;
        private double _debit;
        private Int64 _id;

        #endregion

        public double Debit
        {
            get { return _debit; }
            set
            {
                _debit = value;
                OnPropertyChange(() => Debit);
                Balance = Debit - Credit;
            }
        }

        public double Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChange(() => Credit);
                Balance = Debit - Credit;
            }
        }

        [NotMapped]
        public double Balance
        {
            get { return _balance; }
            set
            {
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
    }
}