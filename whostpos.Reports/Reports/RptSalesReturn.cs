using System.Collections.Generic;
using System.Linq;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSalesReturn.
    /// </summary>
    public partial class RptSalesReturn : Telerik.Reporting.Report
    {
        public RptSalesReturn(Transaction transaction)
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