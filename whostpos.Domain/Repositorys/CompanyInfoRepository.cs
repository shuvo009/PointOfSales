using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class CompanyInfoRepository : ICompanyInfo, IDisposable
    {
        private readonly Dbesm _dbesm;
        private  DbSet<CompanyInformation> _dbSet;

        public CompanyInfoRepository(Dbesm dbesm)
        {
            _dbesm = dbesm;
            _dbSet = _dbesm.Set<CompanyInformation>();
        }

        public void Update(CompanyInformation companyInformation)
        {
            var companyInfo = _dbSet.FirstOrDefault();
            if (companyInfo == null)
                _dbSet.Add(companyInformation);
            else
            {
                _dbSet.Attach(companyInformation);
                _dbesm.Entry(companyInformation).State = EntityState.Modified;
            }
        }

        public CompanyInformation GetCopmanyInformation()
        {
            return _dbSet.FirstOrDefault() ?? new CompanyInformation();
        }

        public void Dispose()
        {
            if (_dbesm != null)
                _dbesm.Dispose();
            _dbSet = null;
        }
    }
}
