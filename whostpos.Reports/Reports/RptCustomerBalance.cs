using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptCustomerBalance.
    /// </summary>
    public partial class RptCustomerBalance : Telerik.Reporting.Report
    {
        public RptCustomerBalance(IEnumerable<Customers> customerses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            CustomerBalanceInfo.DataSource = customerses;
        }
    }
}