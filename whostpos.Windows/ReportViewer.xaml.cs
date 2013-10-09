using MahApps.Metro.Controls;
using Telerik.Reporting;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : MetroWindow
    {
        public ReportViewer(InstanceReportSource instanceReportSource)
        {
            InitializeComponent();

            // Insert code required on object creation below this point.
            ReportHost.ReportSource = instanceReportSource;
        }
    }
}