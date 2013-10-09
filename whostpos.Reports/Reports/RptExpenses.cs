using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptExpenses.
    /// </summary>
    public partial class RptExpenses : Telerik.Reporting.Report
    {
        public RptExpenses(IEnumerable<Expenses> expenseses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ExpensesInfo.DataSource = expenseses;
        }
    }
}