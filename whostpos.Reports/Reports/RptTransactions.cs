using System;
using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptTransactions.
    /// </summary>
    public partial class RptTransactions : Telerik.Reporting.Report
    {
        public RptTransactions(IEnumerable<Transaction> transactions)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            TransactionobjectDataSource.DataSource = transactions;
        }

        private void TransacrionDetailssubReport_ItemDataBinding(object sender, EventArgs e)
        {
            var subReportItem = sender as Telerik.Reporting.Processing.SubReport;
            if (subReportItem == null) return;
            var data = subReportItem.DataObject.RawData as Transaction;

            if (data != null) TransacrionDetailssubReport.ReportSource = new RptTransctionLedger(data.TransactionLedgers);
        }
    }
}