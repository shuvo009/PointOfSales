using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSupplierReport.
    /// </summary>
    public partial class RptSupplierReport : Telerik.Reporting.Report
    {
        public RptSupplierReport(IEnumerable<Suppliers> supplierses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            SupplierInformations.DataSource = supplierses;
        }
    }
}