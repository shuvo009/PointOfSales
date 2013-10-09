using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptBankWithdrawal.
    /// </summary>
    public partial class RptBankWithdrawal : Telerik.Reporting.Report
    {
        public RptBankWithdrawal(IEnumerable<BankTransactions> bankTransactionses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            BankWithdrawalInformation.DataSource = bankTransactionses;
        }
    }
}