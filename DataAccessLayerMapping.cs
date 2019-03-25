using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ICompanyRepositoryFactory : IGenericDataRepository<Company>
    {
    }

    public interface ICompanySettingRepositoryFactory : IGenericDataRepository<CompanySetting>
    {
    }

    public class CompanyRepositoryFactory : ICompanySPFactory<Company>, ICompanyRepositoryFactory
    {
    }

    public class CompanySettingRepositoryFactory : ICompanySPFactory<CompanySetting>, ICompanySettingRepositoryFactory
    {
    }
}
