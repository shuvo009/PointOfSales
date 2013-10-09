using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptCustomerPayment.
    /// </summary>
    public partial class RptCustomerPayment : Telerik.Reporting.Report
    {
        public RptCustomerPayment(IEnumerable<Customers> customerses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}