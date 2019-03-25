using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
 public   interface ICompanyRepository
    {
        IList<Company> GetAllCompanies();
        Company GetCompanyByName(string companyName);
        void AddCompany(params Company[] company);
        void UpdateCompany(params Company[] company);
        void RemoveCompany(params Company[] company);
        IList<CompanySetting> GetCompanySettingsDetails(int companyId);
    }
}
