using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptCutomerLedger.
    /// </summary>
    public partial class RptCutomerLedger : Telerik.Reporting.Report
    {
        public RptCutomerLedger(IEnumerable<CustomerLedger> customerLedgers)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            CustomerLedgerInformation.DataSource = customerLedgers;
        }
    }
}