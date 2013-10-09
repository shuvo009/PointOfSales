using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class OpeningClosingBalance : BaseModel, IEntity
    {
        #region Private Variable

        private double _credit;
        private double _debit;
        private DateTime _entityDate;
        private bool _isInitial;

        #endregion

        public OpeningClosingBalance()
        {
            EntityDate = DateTime.Today;
            Debit =
                Credit = 0;
        }

        [Key]
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

        [Required(ErrorMessage = "Please Enter Debit Amount.")]
        public double Debit
        {
            get { return _debit; }
            set
            {
                _debit = value;
                OnPropertyChange(() => Debit);
            }
        }

        [Required(ErrorMessage = "Please Enter Credit Amount.")]
        public double Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChange(() => Credit);
            }
        }

        public bool IsInitial
        {
            get { return _isInitial; }
            set
            {
                _isInitial = value;
                OnPropertyChange(() => IsInitial);
            }
        }

        [NotMapped]
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}