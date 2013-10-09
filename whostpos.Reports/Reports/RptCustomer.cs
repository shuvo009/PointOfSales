using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptCustomer.
    /// </summary>
    public partial class RptCustomer : Telerik.Reporting.Report
    {
        public RptCustomer(IEnumerable<Customers> customerses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            CustomerInformation.DataSource = customerses;
        }
    }
}