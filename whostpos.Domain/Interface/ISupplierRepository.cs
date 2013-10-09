using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface ISupplierRepository : IBaseRepository<Suppliers>
    {
        ObservableCollection<Suppliers> GetAllSuppliers();
        IQueryable<SuppliersLedger> SearchInLedger(Expression<Func<SuppliersLedger, bool>> expression);
        void SupplierPayment(Int64 supplierId, double amount);
        void AddToSupplierAccount(long supplierId, double amount);
        int TotalSupplier();
    }
}
