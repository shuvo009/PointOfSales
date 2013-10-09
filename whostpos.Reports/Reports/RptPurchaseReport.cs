using System.Collections.Generic;
using System.Linq;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptPurchaseReport.
    /// </summary>
    public partial class RptPurchaseReport : Telerik.Reporting.Report
    {
        public RptPurchaseReport(Transaction transaction)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            TransctionobjectDataSource.DataSource = transaction;
            TransctionLedgerobjectDataSource.DataSource = transaction.TransactionLedgers;
        }
    }
}