using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoredProcedureParameter = DataAccessLayer.StoredProcedureParameter;

namespace PatternEg.Controllers
{
    public class CompanyController : ApiController
    {
        // private CompanySPFactory objCust;
        private ICompanySPFactory objCust;
        public CompanyController()
        {
            this.objCust = new CompanySPFactory();
        }

        [HttpGet]
        [Route("api/Company/GetAll")]
        // GET: Home
        public List<Company> GetAll()
        {
            //int Count = 10;
            //object[] parameters = { Count };
            var test = objCust.GetAll();
            return test.ToList();
        }


        [HttpPost]
        [Route("api/Company/Insert")]
        public int Insert([FromBody] Company model)
        {
            if (ModelState.IsValid)
            {
                var p = new StoredProcedureParameter[]
                {
                    new StoredProcedureParameter(model.CompanyId.GetType(), "CompanyId",model.CompanyId,ParameterDirection.Output),
                    new StoredProcedureParameter(model.CompanyName.GetType(), "CompanyName", model.CompanyName)

                };

                //object[] parameters = {model.CompanyId,model.CompanyName };
                //var p = new List<StoredProcedureParameter>()
                //            {
                //                new StoredProcedureParameter(model.CompanyId.GetType(), "CompanyId", 0),
                //                new StoredProcedureParameter(model.CompanyName.GetType(), "CompanyName", "test cc")

                //            };
             ///   System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("CompanyId", typeof(int));
              //  objCust.ins_Company(OutputParam, "dgfghdfh");
               return objCust.Insert(p);
            }
            return 0;
        }
        [HttpPost]
        [Route("api/Company/InsertLinq")]
        public void InsertLinq([FromBody] Company model)
        {
            if (ModelState.IsValid)
            {
                objCust.AddCompany(model);
            }
           
        }
        [HttpGet]
        [Route("api/Company/GetAllLinq")]
        public IEnumerable<Company> GetAllLinq()
        {
            var result = objCust.GetAllCompanies();
            return result.AsEnumerable();

        }
        //public ActionResult Delete(int id)
        //{
        //    object[] parameters = { id };
        //    this.objCust.Delete(parameters);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Update(int id)
        //{
        //    object[] parameters = { id };
        //    return View(this.objCust.GetbyID(parameters));
        //}

        //[HttpPost]
        //public ActionResult Update(Customer model)
        //{
        //    object[] parameters = { model.Id, model.CustName, model.CustEmail };
        //    objCust.Update(parameters);
        //    return RedirectToAction("Index");
        //}
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
