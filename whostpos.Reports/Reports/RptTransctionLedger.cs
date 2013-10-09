using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptTransctionLedger.
    /// </summary>
    public partial class RptTransctionLedger : Telerik.Reporting.Report
    {
        public RptTransctionLedger(IEnumerable<TransactionLedger> transactionLedgers)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            TransctionLedgerobjectDataSource.DataSource = transactionLedgers;
        }
    }
}