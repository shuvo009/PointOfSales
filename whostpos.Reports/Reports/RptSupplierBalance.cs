using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSupplierBalance.
    /// </summary>
    public partial class RptSupplierBalance : Telerik.Reporting.Report
    {
        public RptSupplierBalance(IEnumerable<Suppliers> supplierses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            SupplierAccountBalance.DataSource = supplierses;
        }
    }
}