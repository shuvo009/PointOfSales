using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using whostpos.Entitys.BaseClass;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Validators;

namespace whostpos.Entitys.Entites
{
    public class Products : BaseModel, IEntity, IDataErrorInfo
    {
        #region Private Variable

        private string _code;
        private string _color;
        private string _description;
        private DateTime _entityDate;
        private Int64 _id;
        private string _model;
        private string _productName;
        private byte[] _photo;
        private string _warrantityDuration;
        private string _dardCode = "0000000";

        #endregion

        [Required(ErrorMessage = "Please Enter ProductName."), MaxLength(100)]
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (_productName == value) return;
                _productName = value;
                OnPropertyChange(() => ProductName);
            }
        }

        [Required(ErrorMessage = "Please Enter Code."), MaxLength(5)]
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChange(() => Code);
            }
        }

        public string BardCode
        {
            get { return _dardCode; }
            set
            {
                if (_dardCode == value) return;
                _dardCode = value;
                OnPropertyChange(() => BardCode);
            }
        }

        [Required(ErrorMessage = "Please enter Model."), MaxLength(100)]
        public string Model
        {
            get { return _model; }
            set
            {
                if (_model == value) return;
                _model = value;
                OnPropertyChange(() => Model);
            }
        }

        [MaxLength(30)]
        public string Color
        {
            get { return _color; }
            set
            {
                if (_color == value) return;
                _color = value;
                OnPropertyChange(() => Color);
            }
        }

        [MaxLength(100)]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChange(() => Description);
            }
        }

        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                if (_photo == value) return;
                _photo = value;
                OnPropertyChange(() => Photo);
            }
        }

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

        [MaxLength(30)]
        public string WarrantityDuration
        {
            get { return _warrantityDuration; }
            set
            {
                if (_warrantityDuration == value) return;
                _warrantityDuration = value;
                OnPropertyChange(() => WarrantityDuration);
            }
        }

        [ForeignKey("Stock")]
        public Int64 StockId { get; set; }

        public virtual Stock Stock { get; set; }

        #region Data Error Info

        public string Error
        {
            get
            {
                var errorMessagess = string.Empty;
                var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var p in properties.Where(p => p.CanWrite && p.CanRead).Where(p => !String.IsNullOrEmpty(this[p.Name])))
                {
                    errorMessagess = "Error";
                }
                return errorMessagess;
            }
        }

        public string this[string columnName]
        {
            get { return InputValidation<Products>.Validate(this, columnName); }
        }

        #endregion

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