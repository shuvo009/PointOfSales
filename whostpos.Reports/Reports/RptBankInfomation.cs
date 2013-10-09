using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptBankInfomation.
    /// </summary>
    public partial class RptBankInfomation : Telerik.Reporting.Report
    {
        public RptBankInfomation(BankAccounts bankAccountses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            BankAccountInformation.DataSource = bankAccountses;
        }
    }
}