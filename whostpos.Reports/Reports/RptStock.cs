using System.Collections.Generic;
using System.Linq;
using whostpos.Entitys.Entites;

namespace whostpos.Reports.Reports
{
    /// <summary>
    /// Summary description for RptStock.
    /// </summary>
    public partial class RptStock : Telerik.Reporting.Report
    {
        public RptStock(IEnumerable<Products> stocks)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            StockInformation.DataSource = stocks;
            TotalProduct.Value = stocks.Count().ToString();
        }
    }
}