using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class CompanyBL : ICompanyRepository
    {
        private readonly ICompanyRepositoryFactory _companyRepository;
        public CompanyBL()
        {
            _companyRepository = new CompanyRepositoryFactory();
           
        }
        public CompanyBL(ICompanyRepositoryFactory companyRepository)
        {
            _companyRepository = companyRepository;
            
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAll();
        }

        public Company GetCompanyByName(string companyName)
        {
            return _companyRepository.GetSingle(
                d => d.CompanyName.Equals(companyName));
              
        }

        public void AddCompany(params Company[] company)
        {
            /* Validation and error handling omitted */
            _companyRepository.Add(company);
        }

        public void UpdateCompany(params Company[] company)
        {
            /* Validation and error handling omitted */
            _companyRepository.Update(company);
        }

        public void RemoveCompany(params Company[] company)
        {
            /* Validation and error handling omitted */
            _companyRepository.Remove(company);
        }

        public IList<CompanySetting> GetCompanySettingsDetails(int companyId)
        {
            var s= _companyRepository.GetSingle(
                d => d.CompanyId.Equals(companyId),
                d => d.CompanySettings);
            return null;
           // throw new NotImplementedException();
        }

        IList<Company> ICompanyRepository.GetAllCompanies()
        {
            throw new NotImplementedException();
        }
    }
}
