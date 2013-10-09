using System;
using whostpos.Entitys.BaseClass;

namespace whostpos.Entitys.Entites
{
    public class AnalisisEntity : BaseModel
    {
        #region Priavte Variable

        private double _amount;
        private string _category;
        private Int64 _productQuantity;

        #endregion

        public Int64 ProductQuantity
        {
            get { return _productQuantity; }
            set
            {
                if (_productQuantity == value) return;
                _productQuantity = value;
                OnPropertyChange("ProductQuantity");
            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChange("Amount");
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if (_category == value) return;
                _category = value;
                OnPropertyChange("Category");
            }
        }
    }
}