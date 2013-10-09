using System.Collections.Generic;
using System.Linq;

namespace whostpos.Reports.Reports
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for BarcodeEM8.
    /// </summary>
    public partial class BarcodeEM8 : Telerik.Reporting.Report
    {
        public BarcodeEM8(string barcode)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            foreach (ReportItemBase reportItemBase in detail.Items)
            {
                if (reportItemBase is Barcode)
                {
                    (reportItemBase as Barcode).Value = barcode;
                }
            }
        }
    }
}