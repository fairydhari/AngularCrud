using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContentManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ContentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpContentsController : ControllerBase
    {
        private  testdbContext _context=new testdbContext();

        private IHostingEnvironment _hostingEnvironment;

        public HelpContentsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //public HelpContentsController(testdbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/HelpContents
        //[HttpGet("GetHelpContents")]
        //public IEnumerable<HelpContent> GetHelpContent(int permissionId, string language)
        //{
        //    //string folderName = "Upload";
        //    //string webRootPath = _hostingEnvironment.ContentRootPath; //_hostingEnvironment.WebRootPath;

        //    //string newPath = Path.Combine(webRootPath, folderName);
        //    if (language == "En")
        //    {
        //       return (from item in _context.HelpContent
        //                     where item.PermissionId == permissionId
        //                     select new HelpContent()
        //                     {
        //                         ContentId = item.ContentId,
        //                         PermissionId = item.PermissionId,
        //                         DescriptionEn = item.DescriptionEn,
        //                         DescriptionAr = null,
        //                         ContentImage = item.ContentImage
        //                     }).ToList();
        //    }
        //  else  if (language == "Ar")
        //    {
        //       return(from item in _context.HelpContent
        //                     where item.PermissionId == permissionId
        //                     select new HelpContent()
        //                     {
        //                         ContentId = item.ContentId,
        //                         PermissionId = item.PermissionId,
        //                         DescriptionEn = null,
        //                         DescriptionAr = item.DescriptionAr,
        //                         ContentImage = item.ContentImage
        //                     }).ToList();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //   // return _context.HelpContent;
        //}
        [HttpGet("GetHelpContents")]
        public IEnumerable<HelpContent> GetHelpContent(int permissionId, string language)
        {
            //string folderName = "Upload";
            //string webRootPath = _hostingEnvironment.ContentRootPath; //_hostingEnvironment.WebRootPath;

            //string newPath = Path.Combine(webRootPath, folderName);
            if (language == "En")
            {
                return (from item in _context.HelpContent
                        where item.PermissionId == permissionId
                        select new HelpContent()
                        {
                            ContentId = item.ContentId,
                            PermissionId = item.PermissionId,
                            DescriptionEn = item.DescriptionEn,
                            DescriptionAr = null,
                            ContentImage = convertFile(item.ContentImage)
                        }).ToList();
            }
            else if (language == "Ar")
            {
                return (from item in _context.HelpContent
                        where item.PermissionId == permissionId
                        select new HelpContent()
                        {
                            ContentId = item.ContentId,
                            PermissionId = item.PermissionId,
                            DescriptionEn = null,
                            DescriptionAr = item.DescriptionAr,
                            ContentImage = convertFile(item.ContentImage)
                        }).ToList();
            }
            else
            {
                return null;
            }
            // return _context.HelpContent;
        }
        private string convertFile(string fileName)
        {
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.ContentRootPath; //_hostingEnvironment.WebRootPath;

            string newPath = Path.Combine(webRootPath, folderName);
            //string path = _hostingEnvironment.WebRootPath + "/Upload/" + fileName;
            byte[] b = System.IO.File.ReadAllBytes(newPath+ "/"+ fileName);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }
        // GET: api/HelpContents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHelpContent([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var helpContent = await _context.HelpContent.FindAsync(id);

            if (helpContent == null)
            {
                return NotFound();
            }

            return Ok(helpContent);
        }

        // PUT: api/HelpContents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelpContent([FromRoute] long id, [FromBody] HelpContent helpContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != helpContent.ContentId)
            {
                return BadRequest();
            }

            _context.Entry(helpContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelpContentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("SaveHelpContent")]
        public async Task<IActionResult> PostHelpContent([FromBody] HelpContent helpContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (helpContent.ContentId > 0)
            {
                if (helpContent.ContentImage != null)
                {

                    _context.Entry(helpContent).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                }
                else
                {
                    HelpContent hc = _context.HelpContent.FirstOrDefault(a => a.ContentId == helpContent.ContentId);
                    hc.DescriptionAr = helpContent.DescriptionAr;
                    hc.DescriptionEn = helpContent.DescriptionEn;
                    _context.SaveChanges();
                }
            }
            else
            { 
                    _context.HelpContent.Add(helpContent);
                    await _context.SaveChangesAsync();
           }

            return CreatedAtAction("GetHelpContent", new { id = helpContent.ContentId }, helpContent);
        }

        // DELETE: api/HelpContents/5
        [HttpDelete("DeleteContent")]
        public async Task<IActionResult> DeleteHelpContent([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var helpContent = await _context.HelpContent.FindAsync(id);
            if (helpContent == null)
            {
                return NotFound();
            }

            _context.HelpContent.Remove(helpContent);
            await _context.SaveChangesAsync();

            return Ok(helpContent);
        }

        private bool HelpContentExists(long id)
        {
            return _context.HelpContent.Any(e => e.ContentId == id);
        }
    }
}