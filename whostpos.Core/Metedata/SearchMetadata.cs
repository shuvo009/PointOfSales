using System;
using GalaSoft.MvvmLight;

namespace whostpos.Core.Metedata
{
    public class SearchMetadata : ViewModelBase
    {
        private string _name;
        private DateTime _fromDate;
        private DateTime _toDate;
        private double _amount;
        private double _quantity;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                RaisePropertyChanged(() => FromDate);
            }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                RaisePropertyChanged(() => ToDate);
            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        public double Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(() => Quantity);
            }
        }
    }
}
