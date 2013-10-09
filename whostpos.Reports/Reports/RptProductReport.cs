using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptProductReport.
    /// </summary>
    public partial class RptProductReport : Telerik.Reporting.Report
    {
        public RptProductReport(IEnumerable<Products> productses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ProductInformation.DataSource = productses;
        }
    }
}