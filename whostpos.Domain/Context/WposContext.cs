using System;
using whostpos.Domain.Interface;
using whostpos.Domain.Repositorys;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Context
{
    public class WposContext : IWposContext
    {
        private readonly Dbesm _dbesm;
        private ICompanyInfo _companyInformation;
        private IProductRepository _productRepository;
        private IBankRepository _bankAccountsRepository;
        private ISupplierRepository _supplierRepository;
        private ICustomerRepository _customerRepository;
        private IOpeningClosingBlanceRepository _openingClosingBlanceRepository;
        private IStockRepository _stockRepository;
        private ITransactionRepository _transactionRepository;
        private IBaseRepository<Expenses> _expensesRepository;
        private IUserAccountRepository _userAccountRepository;

        public WposContext()
            : this(new Dbesm())
        {

        }

        public WposContext(Dbesm dbesm)
        {
            _dbesm = dbesm;
        }

        public ICompanyInfo CompanyInformation
        {
            get { return _companyInformation ?? (_companyInformation = new CompanyInfoRepository(_dbesm)); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_dbesm)); }
        }

        public IBankRepository BankRepository
        {
            get { return _bankAccountsRepository ?? (_bankAccountsRepository = new BankRepository(_dbesm)); }
        }

        public ISupplierRepository SupplierRepository
        {
            get { return _supplierRepository ?? (_supplierRepository = new SupplierRepository(_dbesm)); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository ?? (_customerRepository = new CustomerRepository(_dbesm)); }
        }

        public IOpeningClosingBlanceRepository OpeningClosingBlanceRepository
        {
            get
            {
                return _openingClosingBlanceRepository ??
                       (_openingClosingBlanceRepository = new OpeningClosingBlanceRepository(_dbesm));
            }
        }

        public IStockRepository StockRepository
        {
            get { return _stockRepository ?? (_stockRepository = new StockRepository(_dbesm)); }
        }

        public ITransactionRepository TransactionRepository
        {
            get { return _transactionRepository ?? (_transactionRepository = new TransactionRepository(_dbesm)); }
        }

        public IBaseRepository<Expenses> ExpensesRepository
        {
            get { return _expensesRepository ?? (_expensesRepository = new BaseRepository<Expenses>(_dbesm)); }
        }

        public IUserAccountRepository UserAccountRepository
        {
            get { return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(_dbesm)); }
        }


        public void Save()
        {
            _dbesm.SaveChanges();
        }

        public void Dispose()
        {
            if (_dbesm != null)
                _dbesm.Dispose();
        }
    }
}
