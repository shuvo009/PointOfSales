using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class SuppliersLedger : BaseModel, IEntity
    {
        #region Private Variable

        private double _credit;
        private double _debit;
        private DateTime _entityDate;
        private Int64 _id;
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

        [Required(ErrorMessage = "Please Enter Debit Amount")]
        public double Debit
        {
            get { return _debit; }
            set
            {
                _debit = value;
                OnPropertyChange(() => Debit);
            }
        }

        [Required(ErrorMessage = "Please Enter Credit Amount")]
        public double Credit
        {
            get { return _credit; }
            set
            {
                _credit = value;
                OnPropertyChange(() => Credit);
            }
        }

        [Required(ErrorMessage = "Please Enter Particulars."), MaxLength(100)]
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

        [ForeignKey("Suppliers")]
        public Int64 SuppliersId { get; set; }

        public virtual Suppliers Suppliers { get; set; }

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