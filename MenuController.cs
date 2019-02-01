﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeView.Models;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;
using System.Net;

namespace TreeView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public MenuController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //  private readonly testdbContext _context;
        private testdbContext db = new testdbContext();
        // GET: api/Menu
        [HttpGet("GetTreeList")]
        public IEnumerable<EmployeeHierarchy> Get()
        {
            var employeesHierarchy = GetEmployeesHierachy(db.Menu.AsEnumerable(), null);
            return employeesHierarchy;
            //Menu root = context.Employee
            //.Where(e => e.Parent == null).First();
            //return new string[] { "value1", "value2" };
            /* List<TreeviewItem> hierarchy = new List<TreeviewItem>();
             hierarchy = db.Menu
                             .Where(c => c.ParentId == null)
                             .Select(c => new TreeviewItem()
                             {
                                 MenuId = c.MenuId,
                                 MenuName = c.MenuName,
                                 ParentId = c.ParentId,
                                // Children = HieararchyWalk(c.MenuId)
                             })
                             .ToList();
             return hierarchy;*/
            //HieararchyWalk(hierarchy);

        }
        private IEnumerable<EmployeeHierarchy> GetEmployeesHierachy(IEnumerable<Menu> allEmployees, Menu parentEmployee)
        {
            int? parentEmployeeId = null;

            if (parentEmployee != null)
                parentEmployeeId = parentEmployee.MenuId;

            var childEmployees = allEmployees.Where(e => e.ParentId == parentEmployeeId);

            Collection<EmployeeHierarchy> hierarchy = new Collection<EmployeeHierarchy>();

            foreach (var emp in childEmployees)
                hierarchy.Add(new EmployeeHierarchy() { Employee = emp, Employees = GetEmployeesHierachy(allEmployees, emp) });

            return hierarchy;
        }
        //  private List<>
        public static void HieararchyWalk(List<TreeviewItem> hierarchy)
        {
            if (hierarchy != null)
            {
                foreach (var item in hierarchy)
                {
                    //Console.WriteLine(string.Format("{0} {1}", item.Id, item.Text));
                    HieararchyWalk(item.Children);
                }
            }
        }

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Menu
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Menu/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost, DisableRequestSizeLimit]
        public HttpResponseMessage UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "UploadedImages";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {
               return null;
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
    //public Category GetById(int id)
    //{
    //    var category = db.Menu.Where(e => e.MenuId == null);

    //    this.IncludeParentCategories(category);

    //    return category;
    //}
    //private void IncludeParentCategories(Category category)
    //{
    //    var currentCategory = category;

    //    do
    //    {
    //        this.UnitOfWork.Context.Entry(currentCategory).Reference(e => e.ParentCategory).Load();
    //        currentCategory = currentCategory.ParentCategory;
    //    }
    //    while (currentCategory != null);
    //}
}
}
