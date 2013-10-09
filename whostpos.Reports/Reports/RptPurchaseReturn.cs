using System.Collections.Generic;
using System.Linq;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptPurchaseReturn.
    /// </summary>
    public partial class RptPurchaseReturn : Telerik.Reporting.Report
    {
        public RptPurchaseReturn(Transaction transaction)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            TransctionobjectDataSource.DataSource = transaction;
            TransctionLedgerobjectDataSource.DataSource = transaction.TransactionLedgers;
        }
    }
}