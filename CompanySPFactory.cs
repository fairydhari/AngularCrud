using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CompanySPFactory: ICompanySPFactory
    {
        private IGenericDataRepository<Company> CompanyRepository;

        //CustomerRepository CustRepository;
        public CompanySPFactory()
        {
           
            this.CompanyRepository = new ICompanySPFactory<Company>(new JobPortalEntities());
        }

        public IEnumerable<Company> GetAll()
        {
            string spQuery = "[Get_Company]";
            return CompanyRepository.ExecuteQuery(spQuery, null);
        }

        public Company GetbyID(object[] parameters)
        {
            string spQuery = "[Get_CustomerbyID] {0}";
            return CompanyRepository.ExecuteQuerySingle(spQuery, parameters);
        }

        public int Insert( StoredProcedureParameter[] sqlParameters)
        {

            //SqlParameter[] sqlParams = {
            //        new SqlParameter("@CompanyId",SqlDbType.Int),
            //        new SqlParameter("@CompanyName",SqlDbType.VarChar)

            //};
            //sqlParams[1].Direction = ParameterDirection.Output;
            //sqlParams[1].Value = 0;
            //sqlParams[2].Value = "sdfdaf";
            //SqlParameter[] parameter = {
            //new SqlParameter(...),
            //new SqlParameter(...),
            //new SqlParameter(...)
            //};
           //foreach(var p in sqlParameters)
           // {

           // }
            //SqlParameter[] sqlParams = {
            //new SqlParameter("@CompanyId", SqlDbType.Int,0) {Direction = ParameterDirection.Output},
            //new SqlParameter("@CompanyName", "xsxA"),
            //};
            //            p.ToArray()[0]. = ParameterDirection.Output;
            string spQuery = "ins_Company";
          // var s= CustRepository.ExecuteCommand(spQuery, sqlParams);
            return CompanyRepository.ExecuteSqlCommandWithOutput(spQuery, sqlParameters);
            ///// return CustRepository.ExecuteCommand(spQuery, parameters);
        }
        //public int ins_Company(ObjectParameter companyId, string companyName)
        //{
        //    var companyNameParameter = companyName != null ?
        //        new ObjectParameter("CompanyName", companyName) :
        //        new ObjectParameter("CompanyName", typeof(string));
        
        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_Company", companyId, companyNameParameter);
        //}
        public int Update(object[] parameters)
        {
            string spQuery = "[Update_Customer] {0}, {1}, {2}";
            return CompanyRepository.ExecuteCommand(spQuery, parameters);
        }

        public int Delete(object[] parameters)
        {
            string spQuery = "[Delete_Customer] {0}";
            return CompanyRepository.ExecuteCommand(spQuery, parameters);
        }

        public void AddCompany(params Company[] company)
        {
            CompanyRepository.Add(company);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
           // List<Company> list = CompanyRepository.GetBy<Company,>(dbContext, c => c.Active == true, n => new CampaignWorkTypesSimpleList { Id = n.Id, Name = n.Name });

            return CompanyRepository.GetAll();
        }
    }
}
