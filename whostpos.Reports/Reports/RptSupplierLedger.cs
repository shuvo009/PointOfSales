using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSupplierLedger.
    /// </summary>
    public partial class RptSupplierLedger : Telerik.Reporting.Report
    {
        public RptSupplierLedger(IEnumerable<SuppliersLedger> suppliersLedgers)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            SupplierLedgerInfo.DataSource = suppliersLedgers;
        }
    }
}