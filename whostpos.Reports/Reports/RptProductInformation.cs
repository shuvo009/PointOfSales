using System.Collections.Generic;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptProductInformation.
    /// </summary>
    public partial class RptProductInformation : Telerik.Reporting.Report
    {
        public RptProductInformation(Products productses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ProductInforamtion.DataSource = productses;
        }
    }
}