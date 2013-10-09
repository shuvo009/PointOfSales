using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
  public interface ICompanyInfo
    {
        void Update(CompanyInformation companyInformation);
        CompanyInformation GetCopmanyInformation();
    }
}
