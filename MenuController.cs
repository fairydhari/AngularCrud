using System;
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
using Newtonsoft.Json.Linq;

namespace TreeView.Controllers
{
    [Route("api/[controller]")]
  //  [ApiController]
    [Produces("application/json")]

    public class MenuController : ControllerBase
    {
        private  testdbContext _context=new testdbContext();

        //public MenuController(testdbContext context)
        //{
        //    _context = context;
        //}
        [HttpGet]
        public IEnumerable<Menu> GetMenu()
        {
            return _context.Menu;
        }
        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Menu.SingleOrDefaultAsync(m => m.MenuId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout([FromRoute] int id, [FromBody] Menu workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workout.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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
       
        [HttpPost("saveMenu")]
        public async Task<IActionResult> PostWorkout([FromBody]JObject jsonData)
        {
            dynamic json = jsonData;

            JObject jauthor = json.Menu;
            string token = json.language;
            return Ok("test");
        }
        // [Route("language/{language}")]
        /*  public async Task<IActionResult> PostWorkout([FromBody] Menu workout)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              _context.Menu.Add(workout);
              await _context.SaveChangesAsync();

              return CreatedAtAction("GetWorkout", new { id = workout.MenuId }, workout);
          }*/
        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Menu.SingleOrDefaultAsync(m => m.MenuId == id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }
        [HttpPost("test")]
        public async Task<IActionResult> test([FromBody]JObject json)
        {
            return Ok("test");
        }
        private bool WorkoutExists(int id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
        