using System;
using System.Collections.ObjectModel;
using System.Linq;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        private readonly Dbesm _dbesmBasic;

        public ProductRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
            _dbesmBasic = dbesmBasic;
        }

        public override void Add(Products entity)
        {
            entity.EntityDate = DateTime.Today;
            entity.Stock = new Stock();
            base.Add(entity);
        }

        public string GenerateProductCode()
        {
            Int64 codeNumber = 1;
            Products lastProduct = DbSetBasic.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lastProduct != null)
                codeNumber = lastProduct.Id + 1;
            return codeNumber.ToString("0000");
        }

        public ObservableCollection<string> GetModel()
        {
            return
                new ObservableCollection<string>(DbSetBasic.GroupBy(x => x.Model).Select(x => x.FirstOrDefault().Model));
        }

        public ObservableCollection<string> GetColor()
        {
            return
                new ObservableCollection<string>(DbSetBasic.GroupBy(x => x.Color).Select(x => x.FirstOrDefault().Color));
        }

        public ObservableCollection<Products> GetAllProduucts()
        {
            return
                new ObservableCollection<Products>(DbSetBasic.Where(x => !x.IsDeleted).ToList().Select(x => new Products
                {
                    ProductName = x.ProductName,
                    Photo = x.Photo,
                    Code = x.Code,
                    Id = x.Id,
                    Stock = x.Stock
                }).OrderBy(x => x.Code));
        }

        public int TotalProducts()
        {
            return DbSetBasic.Count(x => !x.IsDeleted);
        }
    }
}