using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptCustomerInformation.
    /// </summary>
    public partial class RptCustomerInformation : Telerik.Reporting.Report
    {
        public RptCustomerInformation(Customers customerses)
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