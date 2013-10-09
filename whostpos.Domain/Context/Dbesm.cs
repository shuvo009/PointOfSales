using System.Data.Entity;
using whost.License.Validator.Entity.Entitys;
using whostpos.Domain.Migrations;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Context
{
    public class Dbesm : DbContext
    {
        public Dbesm()
            : base("WhostPointOfSalesDatabase")
        {
        }

        public DbSet<CompanyInformation> CompanyInformations { get; set; }
        public DbSet<BankAccounts> BankAccountses { get; set; }
        public DbSet<BankTransactions> BankTransactionses { get; set; }
        public DbSet<OpeningClosingBalance> OpeningClosingBalances { get; set; }
        public DbSet<Customers> Customerses { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerLedger> CustomerLedgers { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Products> Productses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Suppliers> Supplierses { get; set; }
        public DbSet<SupplierAccount> SupplierAccounts { get; set; }
        public DbSet<SuppliersLedger> SuppliersLedgers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionLedger> TransactionLedgers { get; set; }

        public DbSet<DbActivitionKey> DbActivitionKeys { get; set; }
        public DbSet<DbActivitionCount> DbActivitionCounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>().HasRequired(c => c.CustomerAccount)
                        .WithMany()
                        .HasForeignKey(x => x.CustomerAccountId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Products>().HasRequired(x => x.Stock)
                        .WithMany()
                        .HasForeignKey(x => x.StockId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Suppliers>().HasRequired(x => x.SupplierAccount)
                        .WithMany()
                        .HasForeignKey(x => x.SupplierAccountId)
                        .WillCascadeOnDelete(false);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Dbesm, Configuration>());
            base.OnModelCreating(modelBuilder);
        }


    }
}