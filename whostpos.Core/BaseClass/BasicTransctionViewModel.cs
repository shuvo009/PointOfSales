using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;
using whostpos.Entitys.Interface;
using whostpos.Entitys.Metadata;

namespace whostpos.Core.BaseClass
{
    public abstract class BasicTransctionViewModel<T> : BasicViewModel<Transaction> where T : class, IEntity, new()
    {
        private TransctionMetadata _transctionMetaData;
        private T _selectedMan;
        private PaymentMethods _paymentMethod;
        private Int64 _bankId;
        private double _dueAmount;
        private readonly IProductList _productList;
        private string _chequeNumber;
        private ObservableCollection<BankAccounts> _bankAccounts;

        protected BasicTransctionViewModel(string title)
            : base(title)
        {
            _productList = new ProductList();
            InitialCommands();
            BankAccounts = WposContext.BankRepository.GetBankAccounts();
            SelectedItem.EntityDate = DateTime.Today;
            PaymentMethod = PaymentMethods.Cash;
            TransctionMetaData = new TransctionMetadata();
        }

        private void InitialCommands()
        {
            SelectProductCommand = new RelayCommand(SelectProductCommandExecute);
            SelectManCommand = new RelayCommand(SelectManCommandExecute);
            AddToInvoiceCommand = new RelayCommand(AddToInvoiceCommandExecute, AddToInvoiceCommandCanExecute);
            RateCalculatorCommand = new RelayCommand(RateCalculatorCommandExecute);
            AmountCalculatorCommand = new RelayCommand(AmountCalculatorCommandExecute);
            RemoveProductFromChatCommand = new RelayCommand<TransactionLedger>(RemoveProductFromChatCommandExecute);
            DuePaymentCommand = new RelayCommand(DuePaymentCommandExecute);
        }

        #region Actions

        public Func<T, bool> AddToInvoiceCommandCanExecuteFunc;
        #endregion

        #region Commands
        public RelayCommand SelectProductCommand { get; private set; }
        public RelayCommand SelectManCommand { get; private set; }
        public RelayCommand AddToInvoiceCommand { get; private set; }
        public RelayCommand RateCalculatorCommand { get; private set; }
        public RelayCommand AmountCalculatorCommand { get; private set; }
        public RelayCommand<TransactionLedger> RemoveProductFromChatCommand { get; private set; }
        public RelayCommand DuePaymentCommand { get; private set; }

        #endregion

        #region CommpandExecute

        private void SelectProductCommandExecute()
        {
            IsBusy = true;
            var selectedProduct = _productList.GetSelectedProduct();
            if (selectedProduct != null)
            {
                TransctionMetaData = new TransctionMetadata
                    {
                        ProductId = selectedProduct.Id,
                        Name = selectedProduct.ProductName,
                        Image =  selectedProduct.Photo
                    };
                if (SelectedItem.IsSales)
                {
                    TransctionMetaData.Rate = selectedProduct.Stock.PurcaseRate;
                    TransctionMetaData.Quantity = selectedProduct.Stock.Quantity;
                    TransctionMetaData.SalesRate = selectedProduct.Stock.SalesRate;
                }
            }
            IsBusy = false;
        }

        public virtual  void AddToInvoiceCommandExecute()
        {
            IsBusy = true;
            var transactionLedgerInfo =
                SelectedItem.TransactionLedgers.FirstOrDefault(x => x.ProductId == TransctionMetaData.ProductId);
            if (transactionLedgerInfo != null)
            {
                transactionLedgerInfo.Quantity += SelectedItem.IsSales ? TransctionMetaData.SalesQuantity : TransctionMetaData.Quantity;
                transactionLedgerInfo.Amount = transactionLedgerInfo.Quantity * TransctionMetaData.Rate;
                transactionLedgerInfo.Rate = TransctionMetaData.Rate;
            }
            else
            {
                SelectedItem.TransactionLedgers.Add(new TransactionLedger
                {
                    Amount = TransctionMetaData.Amount,
                    ProductId = TransctionMetaData.ProductId,
                    Quantity = SelectedItem.IsSales ? TransctionMetaData.SalesQuantity : TransctionMetaData.Quantity,
                    Rate = TransctionMetaData.Rate,
                    SalesRate = TransctionMetaData.SalesRate,
                    Transaction = SelectedItem,
                    Product = WposContext.ProductRepository.FindById(TransctionMetaData.ProductId)
                });
            }
            CalculateSubTotal();
            IsBusy = false;
        }

        private bool AddToInvoiceCommandCanExecute()
        {
            return TransctionMetaData != null && !String.IsNullOrEmpty(TransctionMetaData.Name) &&
                   AddToInvoiceCommandCanExecuteFunc(SelectedMan);
        }

        public abstract void RateCalculatorCommandExecute();

        public abstract void AmountCalculatorCommandExecute();

        public virtual void RemoveProductFromChatCommandExecute(TransactionLedger transactionLedger)
        {
            IsBusy = true;
            var productInfo = SelectedItem.TransactionLedgers.FirstOrDefault(x => x.ProductId == transactionLedger.ProductId);
            if (productInfo == null) return;
            SelectedItem.TransactionLedgers.Remove(productInfo);
            CalculateSubTotal();
            IsBusy = false;
        }

        public abstract void DuePaymentCommandExecute();

        public override void ResetCommandExecute(Transaction entity)
        {
            SelectedMan = new T();
            TransctionMetaData = new TransctionMetadata();
            entity.Supplier = null;
            entity.Customer = null;
            entity.TransactionLedgers = new ObservableCollection<TransactionLedger>();
            if (SelectedItem.IsSales)
                SelectedItem.InvoiceNumber = WposContext.TransactionRepository.GenerateInvoiceNumber();
            base.ResetCommandExecute(entity);
        }

        public abstract void SelectManCommandExecute();

        #endregion

        #region Property

        public TransctionMetadata TransctionMetaData
        {
            get { return _transctionMetaData; }
            set
            {
                _transctionMetaData = value;
                RaisePropertyChanged(() => TransctionMetaData);
            }
        }

        public T SelectedMan
        {
            get { return _selectedMan; }
            set
            {
                _selectedMan = value;
                RaisePropertyChanged(() => SelectedMan);
            }
        }

        public PaymentMethods PaymentMethod
        {
            get { return _paymentMethod; }
            set
            {
                _paymentMethod = value;
                RaisePropertyChanged(() => PaymentMethod);
            }
        }

        public Int64 BankId
        {
            get { return _bankId; }
            set
            {
                _bankId = value;
                RaisePropertyChanged(() => BankId);
            }
        }

        public string ChequeNumber
        {
            get { return _chequeNumber; }
            set
            {
                _chequeNumber = value;
                RaisePropertyChanged(() => ChequeNumber);
            }
        }

        public double DueAmount
        {
            get { return _dueAmount; }
            set
            {
                _dueAmount = value;
                RaisePropertyChanged(() => DueAmount);
            }
        }

        public ObservableCollection<BankAccounts> BankAccounts
        {
            get { return _bankAccounts; }
            set
            {
                _bankAccounts = value;
                RaisePropertyChanged(() => BankAccounts);
            }
        }

        #endregion

        #region Private Methods

        private void CalculateSubTotal()
        {
            SelectedItem.SubTotal = SelectedItem.TransactionLedgers.Sum(x => x.Amount);
        }

        #endregion
    }
}
