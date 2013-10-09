using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class SupplierRepository :BaseRepository<Suppliers>, ISupplierRepository 
    {
        private readonly Dbesm _dbesmBasic;

        public SupplierRepository(Dbesm dbesmBasic) : base(dbesmBasic)
        {
            _dbesmBasic = dbesmBasic;
        }

        public override void Add(Suppliers entity)
        {
            entity.SupplierAccount = new SupplierAccount();
            base.Add(entity);
        }
        public ObservableCollection<Suppliers> GetAllSuppliers()
        {
            return new ObservableCollection<Suppliers>(DbSetBasic.Where(x=>!x.IsDeleted).ToList().Select(x => new Suppliers
            {
                Id = x.Id,
                Name = x.Name,
                Mobile = x.Mobile,
                SupplierAccount = x.SupplierAccount
            }));
        }

        public IQueryable<SuppliersLedger> SearchInLedger(Expression<Func<SuppliersLedger, bool>> expression)
        {
            return DbesmBasic.SuppliersLedgers.Where(expression);
        }

        public void SupplierPayment(long supplierId, double amount)
        {

            var supplierInfo = FindById(supplierId);
            supplierInfo.SupplierAccount.Debit += amount;
            if(supplierInfo.SuppliersLedgers==null)
                supplierInfo.SuppliersLedgers = new Collection<SuppliersLedger>();

            supplierInfo.SuppliersLedgers.Add(new SuppliersLedger
            {
                EntityDate = DateTime.Today,
                Debit = amount,
                Particulars = "Cash Payment"
            });
            IOpeningClosingBlanceRepository openingAndClosingCash = new OpeningClosingBlanceRepository(_dbesmBasic);
            openingAndClosingCash.CashPayment(amount);
        }

        public void AddToSupplierAccount(long supplierId, double amount)
        {
            var supplierInfo = FindById(supplierId);
            supplierInfo.SupplierAccount.Credit += amount;
            if (supplierInfo.SuppliersLedgers == null)
                supplierInfo.SuppliersLedgers = new Collection<SuppliersLedger>();

            supplierInfo.SuppliersLedgers.Add(new SuppliersLedger
            {
                EntityDate = DateTime.Today,
                Credit = amount,
                Particulars = "Purchase Due"
            });
        }

        public int TotalSupplier()
        {
            return DbSetBasic.Count(x => !x.IsDeleted);
        }
    }
}
