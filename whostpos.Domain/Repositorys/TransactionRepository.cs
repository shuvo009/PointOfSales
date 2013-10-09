using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly Dbesm _dbesmBasic;

        public TransactionRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
            _dbesmBasic = dbesmBasic;
        }

        public ObservableCollection<Transaction> GetTransctions(Expression<Func<Transaction, bool>> expression)
        {
            return new ObservableCollection<Transaction>(DbSetBasic.Where(expression));
        }

        public IEnumerable<TransactionLedger> GetTransactionLedger(Int64 transactionId)
        {
            return DbesmBasic.TransactionLedgers.Where(x => x.TransactionId == transactionId).ToList();
        }

        public void PurchaseOnCash(Transaction transaction)
        {
            transaction.PaidTotal = transaction.SubTotal;
            PayWithCash(transaction.PaidTotal);
            AddToStock(transaction.TransactionLedgers);
            AddToTransction(transaction);
        }

        public void PurchaseOnBank(Transaction transaction, Int64 bankId, string chequeNumber)
        {
            IBankRepository bankRepository = new BankRepository(_dbesmBasic);
            var bankTransactions = new BankTransactions
            {
                BankAccountId = bankId,
                ChequeNumber = chequeNumber,
                Withdrawal = transaction.SubTotal,
                Particulars = "Purchas",
            };

            bankRepository.Withdrawal(bankId, bankTransactions);
            AddToStock(transaction.TransactionLedgers);
            transaction.PaidTotal = transaction.SubTotal;
            AddToTransction(transaction);

        }

        public void PurchaseOnDue(Transaction transaction)
        {
            ISupplierRepository supplierRepository = new SupplierRepository(_dbesmBasic);
            if (transaction.SupplierId != null)
                supplierRepository.AddToSupplierAccount((Int64)transaction.SupplierId, transaction.DueTotal);
            if (transaction.PaidTotal > 0)
                PayWithCash(transaction.PaidTotal);
            AddToTransction(transaction);
        }

        public bool SalesOnCash(Transaction transaction)
        {
            transaction.PaidTotal = transaction.NetTotal;
            var stockChecked = CheckStock(transaction.TransactionLedgers);
            if (stockChecked)
            {
                PayWithCash(transaction.PaidTotal);
                AddToTransction(transaction);
                SubtractToStock(transaction.TransactionLedgers);
            }
            return stockChecked;
        }

        public bool SalesOnBank(Transaction transaction, long bankId, string chequeNumber)
        {
            var stockChecked = CheckStock(transaction.TransactionLedgers);
            if (stockChecked)
            {
                IBankRepository bankRepository = new BankRepository(_dbesmBasic);
                var bankTransactions = new BankTransactions
                {
                    BankAccountId = bankId,
                    ChequeNumber = chequeNumber,
                    Deposit = transaction.SubTotal,
                    Particulars = "Sales",
                };

                bankRepository.Deposit(bankId, bankTransactions);
                SubtractToStock(transaction.TransactionLedgers);
                transaction.PaidTotal = transaction.SubTotal;
                AddToTransction(transaction);
            }
            return stockChecked;
        }

        public bool SalesOnDue(Transaction transaction)
        {
            var stockChecked = CheckStock(transaction.TransactionLedgers);
            if (stockChecked)
            {
                ICustomerRepository customerRepository = new CustomerRepository(_dbesmBasic);
                if (transaction.CustomerId != null)
                    customerRepository.AddToCustomerAccount((Int64)transaction.CustomerId, transaction.DueTotal);
                if (transaction.PaidTotal > 0)
                    PayWithCash(transaction.PaidTotal);
                AddToTransction(transaction);
            }
            return stockChecked;
        }

        public string GenerateInvoiceNumber()
        {
            var lastTransction = _dbesmBasic.Transactions.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastTransction == null ? 0.ToString("000000") : (lastTransction.Id + 1).ToString("000000");
        }

        public void SalesReturn(Transaction transaction)
        {
            ICustomerRepository customerRepository = new CustomerRepository(_dbesmBasic);
            if (transaction.CustomerId.HasValue)
                customerRepository.CustomerPayment((Int64)transaction.CustomerId, transaction.SubTotal);
            PayWithCash(transaction.SubTotal);
            AddToStock(transaction.TransactionLedgers);
            AddToTransction(transaction);
        }

        public void PurchaseReturn(Transaction transaction)
        {
            ISupplierRepository supplierRepository = new SupplierRepository(_dbesmBasic);
            if (transaction.SupplierId != null)
                supplierRepository.AddToSupplierAccount((Int64)transaction.SupplierId, transaction.SubTotal);
            PayWithCash(transaction.SubTotal);
            SubtractToStock(transaction.TransactionLedgers);
            AddToTransction(transaction);
        }

        public double TodaysSales()
        {
            return CalculateSalesAmount(DateTime.Today, DateTime.Today);
        }

        public double WeeklySales()
        {
            var delta = DayOfWeek.Saturday - DateTime.Today.DayOfWeek;
            var firstDate = DateTime.Today.AddDays(delta);
            var lastDate = firstDate.AddDays(7);
            return CalculateSalesAmount(firstDate, lastDate);
        }

        public double MonthlySales()
        {
            var firstDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            return CalculateSalesAmount(firstDate, lastDate);
        }

        public int TodayTotalSalesItem()
        {
            return
                DbSetBasic.Where(x => !x.IsDeleted && x.EntityDate == DateTime.Today).ToList()
                    .Sum(x => x.TransactionLedgers.Count);
        }

        #region Private Methods

        private void AddToStock(IEnumerable<TransactionLedger> transactionLedgers)
        {
            foreach (var transactionLedger in transactionLedgers)
            {
                var product = _dbesmBasic.Productses.Find(transactionLedger.ProductId);
                if (product == null)
                    break;
                product.Stock.Quantity += transactionLedger.Quantity;
                product.Stock.PurcaseRate = transactionLedger.Rate;
                product.Stock.SalesRate = transactionLedger.SalesRate;
            }
        }

        private bool CheckStock(IEnumerable<TransactionLedger> transactionLedgers)
        {
            foreach (var transactionLedger in transactionLedgers)
            {
                var product = _dbesmBasic.Productses.Find(transactionLedger.ProductId);
                if (product == null)
                    return false;
                if (product.Stock.Quantity < transactionLedger.Quantity)
                    return false;
            }
            return true;
        }

        private void SubtractToStock(IEnumerable<TransactionLedger> transactionLedgers)
        {
            foreach (var transactionLedger in transactionLedgers)
            {
                var product = _dbesmBasic.Productses.Find(transactionLedger.ProductId);
                if (product == null)
                    break;
                product.Stock.Quantity -= transactionLedger.Quantity;
            }
        }

        private void AddToTransction(Transaction transaction)
        {
            _dbesmBasic.Transactions.Add(transaction);
        }

        private void PayWithCash(double amount)
        {
            IOpeningClosingBlanceRepository openingClosingBlanceRepository = new OpeningClosingBlanceRepository(_dbesmBasic);
            openingClosingBlanceRepository.CashPayment(amount);
        }

        private double CalculateSalesAmount(DateTime since, DateTime untile)
        {
            return DbSetBasic.Where(x => !x.IsDeleted && x.EntityDate >= since && x.EntityDate <= untile).ToList().Sum(x => x.PaidTotal);
        }

        #endregion
    }
}