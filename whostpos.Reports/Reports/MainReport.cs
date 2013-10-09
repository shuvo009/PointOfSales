using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for MainReport.
    /// </summary>
    public partial class MainReport : Telerik.Reporting.Report
    {
        public MainReport(string reportTitle, CompanyInformation companyInformations)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ReportHeaderInformation.DataSource = companyInformations;
            TextReportTitle.Value = reportTitle;
        }
    }
}