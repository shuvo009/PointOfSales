using System;
using System.Collections.ObjectModel;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface IProductRepository : IBaseRepository<Products>
    {
        string GenerateProductCode();
        ObservableCollection<String> GetModel();
        ObservableCollection<String> GetColor();
        ObservableCollection<Products> GetAllProduucts();
        int TotalProducts();
    }
}
