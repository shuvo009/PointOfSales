using System.Collections.Generic;
using System.Linq;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSales.
    /// </summary>
    public partial class RptSales : Telerik.Reporting.Report
    {
        public RptSales(Transaction transaction)
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