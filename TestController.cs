using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
//using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.AspNetCore.Hosting.Internal;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Xml.Linq;
using System.Net;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public TestController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        [HttpGet("GetEmployeeList")]
        // public HttpResponseMessage Get()
        public List<Employee> Get()

        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "samples\\data.xml");

            // string path = HostingEnvironment.MapPath(@"~/Reports/xsl/labels_single.xsl"); //HostingEnvironment.MapPath("~/samples/data.xml");
            XDocument doc = XDocument.Load(path);
            var query = (from c in doc.Descendants("employee")
                         select new Employee()
                         {
                             ID = (string)c.Element("id"),
                             Cmp_Name = (string)c.Element("cmp_name"),
                             Address = (string)c.Element("address")
                         }).ToList();

            IEnumerable<XElement> authors1 = doc.Descendants("documentelement")

                .Descendants("employee");
            IEnumerable <XElement> authors = doc.Descendants("documentelement");
            var s = authors.ToList();
            var customers = doc.Descendants("documentelement").Elements("employee");
            return query;
            //XDocument doc = new XDocument();
            //doc.Load(path);

            //    HttpResponseMessage response = this.Request.
            //CreateResponse(HttpStatusCode.OK);
            //    response.Content = new StringContent
            //(doc.OuterXml, Encoding.UTF8, "application/xml");
            //    return response;
            //}
        }
        // GET api/values/5
       // [HttpGet("{id}")]
        [HttpGet("GetEmployee/{key}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "samples\\data.xml");

            // string path = HostingEnvironment.MapPath(@"~/Reports/xsl/labels_single.xsl"); //HostingEnvironment.MapPath("~/samples/data.xml");
            XDocument doc = XDocument.Load(path);
            var book = doc.Descendants("employee").FirstOrDefault();
            book.Add(new XElement("employee",
                 new XElement("id", 5),
                 new XElement("cmp_name", "test"),
                    
                 new XElement("address", "tvm")
                  
                 ));

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("EmployeeDelete/{id}")]
        public void Delete(string id)
        {
        }
    }
}
