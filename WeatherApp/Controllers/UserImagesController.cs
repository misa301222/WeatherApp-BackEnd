using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.EntityFrameworkCore;
using WeatherApp.Models.Catalog;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserImage>>> GetUserImage()
        {
            return await _context.UserImage.ToListAsync();
        }

        // GET: api/UserImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserImage>> GetUserImage(string id)
        {
            var userImage = await _context.UserImage.FindAsync(id);

            if (userImage == null)
            {
                return NotFound();
            }

            return userImage;
        }

        // PUT: api/UserImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserImage(string id, UserImage userImage)
        {
            if (id != userImage.Email)
            {
                return BadRequest();
            }

            _context.Entry(userImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserImageExists(id))
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

        // POST: api/UserImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserImage>> PostUserImage(UserImage userImage)
        {
            _context.UserImage.Add(userImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserImageExists(userImage.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserImage", new { id = userImage.Email }, userImage);
        }

        // DELETE: api/UserImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserImage(string id)
        {
            var userImage = await _context.UserImage.FindAsync(id);
            if (userImage == null)
            {
                return NotFound();
            }

            _context.UserImage.Remove(userImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserImageExists(string id)
        {
            return _context.UserImage.Any(e => e.Email == id);
        }
    }
}
