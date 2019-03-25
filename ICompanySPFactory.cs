using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public interface ICompanySPFactory
    {
        int Insert(StoredProcedureParameter[] sqlParameters);
        int Update(object[] parameters);
        int Delete(object[] parameters);
        IEnumerable<Company> GetAll();
        void AddCompany(params Company[] company);
        IEnumerable<Company> GetAllCompanies();
    }
}
