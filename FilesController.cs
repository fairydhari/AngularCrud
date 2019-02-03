using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApiCore.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/files/sample.png
        [HttpGet("{fileName}")]
        public string Get(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "/Upload/" + fileName;
            byte[] b = System.IO.File.ReadAllBytes(path);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }
    }
}