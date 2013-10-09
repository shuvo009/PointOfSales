using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptBankDeposit.
    /// </summary>
    public partial class RptBankDeposit : Telerik.Reporting.Report
    {
        public RptBankDeposit(IEnumerable<BankTransactions> bankTransactionses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            BankDepositInfo.DataSource = bankTransactionses;
        }
    }
}