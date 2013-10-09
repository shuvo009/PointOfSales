using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whost.License.Validator.Activitor;
using whostpos.Core.BaseClass;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Others
{
    public class HomeViewModel : BaseViewModel
    {
        private int _totalProductItems;
        private long _totalProductinStock;
        private int _totalCustomer;
        private int _totalSupplier;
        private int _totalBankAccount;
        private double _totalSalesToday;
        private double _totalSalesWeekly;
        private double _totalSalesMonthly;
        private int _todayTotalSalesItem;
        private CompanyInformation _companyInformation;
        private string _daysLeft;
        private string _licenseTo;

        public HomeViewModel()
            : base("Home")
        {
            //LoadData();
        }

        #region Property
        public int TotalProductItems
        {
            get { return _totalProductItems; }
            set
            {
                _totalProductItems = value;
                RaisePropertyChanged(() => TotalProductItems);
            }
        }

        public Int64 TotalProductinStock
        {
            get { return _totalProductinStock; }
            set
            {
                _totalProductinStock = value;
                RaisePropertyChanged(() => TotalProductinStock);
            }
        }

        public int TotalCustomer
        {
            get { return _totalCustomer; }
            set
            {
                _totalCustomer = value;
                RaisePropertyChanged(() => TotalCustomer);
            }
        }

        public int TotalSupplier
        {
            get { return _totalSupplier; }
            set
            {
                _totalSupplier = value;
                RaisePropertyChanged(() => TotalSupplier);
            }
        }

        public int TotalBankAccount
        {
            get { return _totalBankAccount; }
            set
            {
                _totalBankAccount = value;
                RaisePropertyChanged(() => TotalBankAccount);
            }
        }

        public Double TotalSalesToday
        {
            get { return _totalSalesToday; }
            set
            {
                _totalSalesToday = value;
                RaisePropertyChanged(() => TotalSalesToday);
            }
        }

        public Double TotalSalesWeekly
        {
            get { return _totalSalesWeekly; }
            set
            {
                _totalSalesWeekly = value;
                RaisePropertyChanged(() => TotalSalesWeekly);
            }
        }

        public Double TotalSalesMonthly
        {
            get { return _totalSalesMonthly; }
            set
            {
                _totalSalesMonthly = value;
                RaisePropertyChanged(() => TotalSalesMonthly);
            }
        }

        public int TodayTotalSalesItem
        {
            get { return _todayTotalSalesItem; }
            set
            {
                _todayTotalSalesItem = value;
                RaisePropertyChanged(() => TodayTotalSalesItem);
            }
        }

        public string DaysLeft
        {
            get { return String.Format("{0} Days Left.", _daysLeft); }
            set
            {
                _daysLeft = value;
                RaisePropertyChanged(() => DaysLeft);
            }
        }

        public CompanyInformation CompanyInformation
        {
            get { return _companyInformation; }
            set
            {
                _companyInformation = value;
                RaisePropertyChanged(() => CompanyInformation);
            }
        }

        public string LicenseTo
        {
            get { return String.Format("License To {0}", _licenseTo); }
            set
            {
                _licenseTo = value;
                RaisePropertyChanged(() => LicenseTo);
            }
        }

        #endregion

        private async void LoadData()
        {
            IsBusy = true;
            await Task.Factory.StartNew(() =>
            {
                using (IWposContext wposContext = new WposContext())
                {
                    TotalProductItems = wposContext.ProductRepository.TotalProducts();
                    TotalProductinStock = wposContext.StockRepository.TotalStock();
                    TotalCustomer = wposContext.CustomerRepository.TotalCustomer();
                    TotalSupplier = wposContext.SupplierRepository.TotalSupplier();
                    TotalBankAccount = wposContext.BankRepository.TotalBankAcount();
                    TotalSalesToday = wposContext.TransactionRepository.TodayTotalSalesItem();
                    TotalSalesWeekly = wposContext.TransactionRepository.WeeklySales();
                    TotalSalesMonthly = wposContext.TransactionRepository.MonthlySales();
                    TodayTotalSalesItem = wposContext.TransactionRepository.TodayTotalSalesItem();
                    CompanyInformation = wposContext.CompanyInformation.GetCopmanyInformation();
                    var initiallicense = new InitialLicense();
                    DaysLeft = initiallicense.DaysLeft().ToString(CultureInfo.InvariantCulture);
                    LicenseTo = initiallicense.LicenseTo();
                    IsBusy = false;
                }
            });
        }
    }
}
