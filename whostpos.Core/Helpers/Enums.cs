namespace whostpos.Core.Helpers
{
    public enum MessageBoxImage
    {
        Ok, Error, Question
    }

    public enum SearchOptions
    {
        SingleDate,
        DateRange,
        Name,
        Amount,
        Quantity,
        All
    }

    public enum ReportType
    {
        PrintBarcode,
        CustomerBalance,
        Customer,
        CustomerLedger,
        CustomerDuePAyment,
        CustomerReport,
        BankAccount,
        BankDepositReport,
        BankWithdrawalReport,
        Product,
        ProductPreport,
        Supplier,
        SupplierBalance,
        SupplierLedger,
        SupplierDuePayment,
        SupplierReport,
        Stock,
        StockRegister,
        ExpensesReport,
        Sales,
        SalesReport,
        SalesReturn, 
        SalesReturnReport,
        Purchase,      
        PurchaseReport,
        PurchaseReturn,
        PurchaseReturnReport,
    }

    public enum PaymentMethods
    {
        Cash,
        Due,
        Bank
    }

}
