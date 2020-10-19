using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.Controllers
{
    
    public class AppUsersController : BaseApiController
    {
        private readonly DataContext _context;

        public AppUsersController(DataContext context)
        {
            _context = context;
            
        }

        // GET: api/AppUsers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            return Ok(await _context.User.ToListAsync());
        }

        // GET: api/AppUsers/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(int id)
        {
            var appUser = await _context.User.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(int id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST: api/AppUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            _context.User.Add(appUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        [HttpPost ("Batch")]
        public async Task<ActionResult<AppUser>> BatchAppUser(List<AppUser> appUser)
        {

            _context.User.AddRange(appUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser",null);
        }
        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
        {
            var appUser = await _context.User.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.User.Remove(appUser);
            await _context.SaveChangesAsync();

            return appUser;
        }

        private bool AppUserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
