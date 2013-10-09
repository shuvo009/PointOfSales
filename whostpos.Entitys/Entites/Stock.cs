using System;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;

namespace whostpos.Entitys.Entites
{
    public class Stock : BaseModel, IEntity
    {
        #region Private Variable

        private Int64 _id;
        private double _purcaseRate;
        private int _quantity;
        private double _salesRate;

        #endregion

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

        public double SalesRate
        {
            get { return _salesRate; }
            set
            {
                _salesRate = value;
                OnPropertyChange(() => SalesRate);
            }
        }

        public double PurcaseRate
        {
            get { return _purcaseRate; }
            set
            {
                _purcaseRate = value;
                OnPropertyChange(() => PurcaseRate);
            }
        }

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