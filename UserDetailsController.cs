using webapieg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace webapieg.Controllers
{
    [Route("api/user")]
    public class UserDetailsController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(UserDetails))]
        [ActionName("PostUserDetails")]
        [Route("api/user/PostUserDetails")]
        // POST api/user/PostUserDetails
        public IHttpActionResult PostUserDetails([FromBody]UserDetails userDetails)
        {
            return null;
        }
        [HttpPut]
        [ActionName("PutUserDetails")]
        // PUT api/user/PutUserDetails
        public IHttpActionResult PutUserDetails(int userId, UserDetails userDetails)
        {
            
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPost]
        public IHttpActionResult UserLogin(UserDetails userDetails)
        {
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
