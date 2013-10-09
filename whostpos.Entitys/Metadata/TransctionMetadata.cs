using System;
using whostpos.Entitys.BaseClass;

namespace whostpos.Entitys.Metadata
{
    public class TransctionMetadata : BaseModel
    {
        private string _name;
        private double _salesRate;
        private int _quantity;
        private double _rate;
        private double _amount;
        private long _productId;
        private int _salesQuantity;
        private byte[] _image;

        public Int64 ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChange(() => ProductId);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange(() => Name);
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

        public int SalesQuantity
        {
            get { return _salesQuantity; }
            set
            {
                _salesQuantity = value;
                OnPropertyChange(() => SalesQuantity);
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChange(() => Quantity);
            }
        }

        public double Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChange(() => Rate);
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

        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChange(() => Image);
            }
        }
    }
}
