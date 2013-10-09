/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DPOS"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using whostpos.Core.Classes;
using whostpos.Core.Interface;
using whostpos.ViewModel.Bank;
using whostpos.ViewModel.Cash;
using whostpos.ViewModel.Customer;
using whostpos.ViewModel.Database;
using whostpos.ViewModel.Expenses;
using whostpos.ViewModel.Others;
using whostpos.ViewModel.Products;
using whostpos.ViewModel.Stock;
using whostpos.ViewModel.Supplier;
using whostpos.ViewModel.Transction;
using whostpos.ViewModel.UserAccount;

namespace whostpos.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        private static string _currentKey = System.Guid.NewGuid().ToString();
        public static string CurrentKey
        {
            get
            {
                return _currentKey;
            }
            private set
            {
                _currentKey = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                if (!SimpleIoc.Default.IsRegistered<IImageBrowser>())
                    SimpleIoc.Default.Register<IImageBrowser, ImageBrowser>();

                if (!SimpleIoc.Default.IsRegistered<ICustomerList>())
                    SimpleIoc.Default.Register<ICustomerList, CustomerList>();

                if (!SimpleIoc.Default.IsRegistered<ISupplierList>())
                    SimpleIoc.Default.Register<ISupplierList, SupplierList>();
            }
            else
            {
                // Create run time view services and models
                if (!SimpleIoc.Default.IsRegistered<IImageBrowser>())
                    SimpleIoc.Default.Register<IImageBrowser, ImageBrowser>();

                if (!SimpleIoc.Default.IsRegistered<ICustomerList>())
                    SimpleIoc.Default.Register<ICustomerList, CustomerList>();

                if (!SimpleIoc.Default.IsRegistered<ISupplierList>())
                    SimpleIoc.Default.Register<ISupplierList, SupplierList>();
            }

            RegisterViewModels();
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CompanyViewModel>();
            SimpleIoc.Default.Register<ProductSetupViewModel>();
            SimpleIoc.Default.Register<BankAcountViewModel>();
            SimpleIoc.Default.Register<SupplierViewModel>();
            SimpleIoc.Default.Register<CustomerViewModel>();
            SimpleIoc.Default.Register<CustomeBalanceViewModel>();
            SimpleIoc.Default.Register<CutomerLedgerViewModel>();
            SimpleIoc.Default.Register<CustomerDuePaymentViewModel>();
            SimpleIoc.Default.Register<SupplierBalanceViewModel>();
            SimpleIoc.Default.Register<SupplierLedgerViewModel>();
            SimpleIoc.Default.Register<SupplierDuePaymentViewModel>();
            SimpleIoc.Default.Register<StockViewModel>();
            SimpleIoc.Default.Register<PurchaseReportViewModel>();
            SimpleIoc.Default.Register<BankDepositViewModel>();
            SimpleIoc.Default.Register<BankWithdrawalViewModel>();
            SimpleIoc.Default.Register<ProductReportViewModel>();
            SimpleIoc.Default.Register<CustomerReportViewModel>();
            SimpleIoc.Default.Register<SupplierReportViewModel>();
            SimpleIoc.Default.Register<BankDepositReportViewModel>();
            SimpleIoc.Default.Register<BankWithdrawalReportViewModel>();
            SimpleIoc.Default.Register<SalesReportViewModel>();
            SimpleIoc.Default.Register<CashReportViewModel>();
            SimpleIoc.Default.Register<SalesReturnReportViewModel>();
            SimpleIoc.Default.Register<PurchaseViewModel>();
            SimpleIoc.Default.Register<SalesViewModel>();
            SimpleIoc.Default.Register<SalesReturnViewModel>();
            SimpleIoc.Default.Register<PurchaseReturnViewModel>();
            SimpleIoc.Default.Register<UserAccountViewModel>();
            SimpleIoc.Default.Register<DatabaseRestoreBackupViewModel>();
            SimpleIoc.Default.Register<InitialBalanceViewModel>();
            SimpleIoc.Default.Register<ExpenceViewModel>();
            SimpleIoc.Default.Register<ExpensesReportViewModel>();
            SimpleIoc.Default.Register<PurchaseReturnReportViewModel>();
            SimpleIoc.Default.Register<ChangePasswodViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>(CurrentKey);
            }
        }

        public CompanyViewModel CompanyViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CompanyViewModel>(CurrentKey); }
        }

        public ProductSetupViewModel ProductSetupViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ProductSetupViewModel>(CurrentKey); }
        }

        public BankAcountViewModel BankAcountViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BankAcountViewModel>(CurrentKey); }
        }

        public SupplierViewModel SupplierViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SupplierViewModel>(CurrentKey); }
        }

        public CustomerViewModel CustomerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CustomerViewModel>(CurrentKey); }
        }

        public CustomeBalanceViewModel CustomeBalanceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CustomeBalanceViewModel>(CurrentKey); }
        }

        public CutomerLedgerViewModel CutomerLedgerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CutomerLedgerViewModel>(CurrentKey); }
        }

        public CustomerDuePaymentViewModel CustomerDuePaymentViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CustomerDuePaymentViewModel>(CurrentKey); }
        }

        public SupplierBalanceViewModel SupplierBalanceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SupplierBalanceViewModel>(CurrentKey); }
        }

        public SupplierLedgerViewModel SupplierLedgerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SupplierLedgerViewModel>(CurrentKey); }
        }

        public SupplierDuePaymentViewModel SupplierDuePaymentViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SupplierDuePaymentViewModel>(CurrentKey); }
        }

        public StockViewModel StockViewModel
        {
            get { return ServiceLocator.Current.GetInstance<StockViewModel>(CurrentKey); }
        }

        public PurchaseReportViewModel PurchaseReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PurchaseReportViewModel>(CurrentKey); }
        }

        public BankDepositViewModel BankDepositViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BankDepositViewModel>(CurrentKey); }
        }

        public BankWithdrawalViewModel BankWithdrawalViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BankWithdrawalViewModel>(CurrentKey); }
        }

        public ProductReportViewModel ProductReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ProductReportViewModel>(CurrentKey); }
        }

        public CustomerReportViewModel CustomerReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CustomerReportViewModel>(CurrentKey); }
        }

        public SupplierReportViewModel SupplierReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SupplierReportViewModel>(CurrentKey); }
        }

        public BankDepositReportViewModel BankDepositReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BankDepositReportViewModel>(CurrentKey); }
        }

        public BankWithdrawalReportViewModel BankWithdrawalReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BankWithdrawalReportViewModel>(CurrentKey); }
        }

        public SalesReportViewModel SalesReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SalesReportViewModel>(CurrentKey); }
        }

        public CashReportViewModel CashReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CashReportViewModel>(CurrentKey); }
        }

        public SalesReturnReportViewModel SalesReturnReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SalesReturnReportViewModel>(CurrentKey); }
        }

        public PurchaseViewModel PurchaseViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PurchaseViewModel>(CurrentKey); }
        }

        public SalesViewModel SalesViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SalesViewModel>(CurrentKey); }
        }

        public SalesReturnViewModel SalesReturnViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SalesReturnViewModel>(CurrentKey); }
        }

        public PurchaseReturnViewModel PurchaseReturnViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PurchaseReturnViewModel>(CurrentKey); }
        }

        public UserAccountViewModel UserAccountViewModel
        {
            get { return ServiceLocator.Current.GetInstance<UserAccountViewModel>(CurrentKey); }
        }

        public DatabaseRestoreBackupViewModel DatabaseRestoreBackupViewModel
        {
            get { return ServiceLocator.Current.GetInstance<DatabaseRestoreBackupViewModel>(CurrentKey); }
        }

        public InitialBalanceViewModel InitialBalanceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<InitialBalanceViewModel>(CurrentKey); }
        }

        public ExpenceViewModel ExpenceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ExpenceViewModel>(CurrentKey); }
        }

        public ExpensesReportViewModel ExpensesReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ExpensesReportViewModel>(CurrentKey); }
        }

        public PurchaseReturnReportViewModel PurchaseReturnReportViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PurchaseReturnReportViewModel>(CurrentKey); }

        }

        public ChangePasswodViewModel ChangePasswodViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ChangePasswodViewModel>(CurrentKey); }
        }

        public HomeViewModel HomeViewModel
        {
            get { return ServiceLocator.Current.GetInstance<HomeViewModel>(CurrentKey); }
        }

        public static void Cleanup()
        {
            if (SimpleIoc.Default.IsRegistered<SalesViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SalesViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<MainViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<MainViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CompanyViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CompanyViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<ProductSetupViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<ProductSetupViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<BankAcountViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<BankAcountViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SupplierViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SupplierViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CustomerViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CustomerViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CustomeBalanceViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CustomeBalanceViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CutomerLedgerViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CutomerLedgerViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CustomerDuePaymentViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CustomerDuePaymentViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SupplierBalanceViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SupplierBalanceViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SupplierLedgerViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SupplierLedgerViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SupplierDuePaymentViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SupplierDuePaymentViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<StockViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<StockViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<PurchaseReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<PurchaseReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<BankDepositViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<BankDepositViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<BankWithdrawalViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<BankWithdrawalViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<ProductReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<ProductReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CustomerReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CustomerReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SupplierReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SupplierReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<BankDepositReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<BankDepositReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<BankWithdrawalReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<BankWithdrawalReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SalesReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SalesReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<CashReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<CashReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SalesReturnReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SalesReturnReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<PurchaseViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<PurchaseViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SalesViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SalesViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<SalesReturnViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<SalesReturnViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<PurchaseReturnViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<PurchaseReturnViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<UserAccountViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<UserAccountViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<DatabaseRestoreBackupViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<DatabaseRestoreBackupViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<InitialBalanceViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<InitialBalanceViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<ExpenceViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<ExpenceViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<ExpensesReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<ExpensesReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<PurchaseReturnReportViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<PurchaseReturnReportViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<ChangePasswodViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<ChangePasswodViewModel>(CurrentKey);
            if (SimpleIoc.Default.IsRegistered<HomeViewModel>(CurrentKey))
                SimpleIoc.Default.Unregister<HomeViewModel>(CurrentKey);
            CurrentKey = System.Guid.NewGuid().ToString();
        }

        public static void RegisterBacktagViewModels()
        {
            //if (!SimpleIoc.Default.IsRegistered<HomeViewModel>(CurrentKey))
            //    SimpleIoc.Default.Register<>();<HomeViewModel>(CurrentKey);
        }
    }
}