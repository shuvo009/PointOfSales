using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptSupplierInformation.
    /// </summary>
    public partial class RptSupplierInformation : Telerik.Reporting.Report
    {
        public RptSupplierInformation(Suppliers supplierses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            SupplierInformation.DataSource = supplierses;
        }
    }
}