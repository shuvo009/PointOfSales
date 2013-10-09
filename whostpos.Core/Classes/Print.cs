using System;
using System.Collections.Generic;
using Telerik.Reporting;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Domain.Context;
using whostpos.Entitys.Entites;
using whostpos.Reports.Reports;
using whostpos.Windows;

namespace whostpos.Core.Classes
{
    class Print : IPrinter
    {
        public void Printer<T>(ReportType reportType, T printingData)
        {
            Report report = null;
            string title;
            switch (reportType)
            {
                case ReportType.CustomerLedger:
                    title = "Customer Ledger";
                    report = new RptCutomerLedger(printingData as IEnumerable<CustomerLedger>);
                    break;
                case ReportType.CustomerBalance:
                    title = "Customer Balance";
                    report = new RptCustomerBalance(printingData as IEnumerable<Customers>);
                    break;
                case ReportType.CustomerReport:
                    title = "Customer's";
                    report = new RptCustomer(printingData as IEnumerable<Customers>);
                    break;
                case ReportType.Customer:
                    title = "Customer Information";
                    report = new RptCustomerInformation(printingData as Customers);
                    break;
                case ReportType.BankAccount:
                    title = "Bank Information";
                    report = new RptBankInfomation(printingData as BankAccounts);
                    break;
                case ReportType.BankDepositReport:
                    title = "Bank Deposit";
                    report = new RptBankDeposit(printingData as IEnumerable<BankTransactions>);
                    break;
                case ReportType.BankWithdrawalReport:
                    title = "Bank Withdrawal";
                    report = new RptBankWithdrawal(printingData as IEnumerable<BankTransactions>);
                    break;
                case ReportType.Product:
                    title = "Product";
                    report = new RptProductInformation(printingData as Products);
                    break;
                case ReportType.ProductPreport:
                    title = "Product's";
                    report = new RptProductReport(printingData as IEnumerable<Products>);
                    break;
                case ReportType.Supplier:
                    title = "Supplier Information";
                    report = new RptSupplierInformation(printingData as Suppliers);
                    break;
                case ReportType.SupplierBalance:
                    title = "Supplier Balance";
                    report = new RptSupplierBalance(printingData as IEnumerable<Suppliers>);
                    break;
                case ReportType.SupplierLedger:
                    title = "Supplier Ledger";
                    report = new RptSupplierLedger(printingData as IEnumerable<SuppliersLedger>);
                    break;
                case ReportType.SupplierReport:
                    title = "Supplier's";
                    report = new RptSupplierReport(printingData as IEnumerable<Suppliers>);
                    break;
                case ReportType.Stock:
                    title = "Stock";
                    report = new RptStock(printingData as IEnumerable<Products>);
                    break;
                case ReportType.ExpensesReport:
                    title = "Expense";
                    report = new RptExpenses(printingData as IEnumerable<Expenses>);
                    break;
                case ReportType.Sales:
                    title = "Sales";
                    report = new RptSales(printingData as Transaction);
                    break;
                case ReportType.SalesReport:
                    title = "Sales Report";
                    report = new RptTransactions(printingData as IEnumerable<Transaction>);
                    break;
                case ReportType.SalesReturn:
                    title = "Sales Return";
                    report = new RptSalesReturn(printingData as Transaction);
                    break;
                case ReportType.SalesReturnReport:
                    title = "Sales Return";
                    report = new RptTransactions(printingData as IEnumerable<Transaction>);
                    break;

                case ReportType.Purchase:
                    title = "Purchase";
                    report = new RptPurchaseReport(printingData as Transaction);
                    break;
                case ReportType.PurchaseReport:
                    title = "Purchase Report";
                    report = new RptTransactions(printingData as IEnumerable<Transaction>);
                    break;
                case ReportType.PurchaseReturn:
                    title = "Purchase Return";
                    report = new RptPurchaseReturn(printingData as Transaction);
                    break;
                case ReportType.PurchaseReturnReport:
                    title = "Purchase Return";
                    report = new RptTransactions(printingData as IEnumerable<Transaction>);
                    break;
                case ReportType.PrintBarcode:
                    title = "Bar code";
                    report = new BarcodeEM8(printingData as string);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("reportType");
            }
            var companyInfo = new WposContext().CompanyInformation.GetCopmanyInformation();
            var mainReport = new MainReport(title,companyInfo) { ReportHoster = { ReportSource = report } };
            var reportViewer = new ReportViewer(new InstanceReportSource { ReportDocument = mainReport });
            reportViewer.ShowDialog();
        }
    }
}
