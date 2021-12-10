using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.EntityFrameworkCore;
using WeatherApp.Models.BindingModel;
using WeatherApp.Models.Catalog;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reaction>>> GetReaction()
        {
            return await _context.Reaction.ToListAsync();
        }

        // GET: api/Reactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reaction>> GetReaction(string id)
        {
            var reaction = await _context.Reaction.FindAsync(id);

            if (reaction == null)
            {
                return NotFound();
            }

            return reaction;
        }

        [HttpGet("GetReactionByEmailAndNewsId/{email}/{newsId}")]
        public async Task<ActionResult<Reaction>> GetReactionByEmailAndNewsId(string email, int newsId)
        {
            var reaction = await _context.Reaction.Where(x => x.Email == email && x.NewsId == newsId).SingleOrDefaultAsync();

            if (reaction == null)
            {
                return NotFound();
            }

            return reaction;
        }


        [HttpGet("GetReactionByNewsId/{newsId}")]
        public async Task<object> GetReactionByNewsId(int newsId)
        {
            var reaction = await _context.Reaction.Where(x => x.NewsId == newsId).ToListAsync();
            if (reaction == null)
            {
                return NoContent();
            }


            var totalLike = 0;
            var totalWow = 0;
            var totalSad = 0;
            var totalAngry = 0;

            foreach (var r in reaction)
            {
                totalLike += r.ReactionLike;
                totalWow += r.ReactionWow;
                totalSad += r.ReactionSad;
                totalAngry += r.ReactionAngry;
            }

            UpdateReactionNewsBindingModel reactionResults = new UpdateReactionNewsBindingModel(newsId, totalLike, totalWow, totalSad, totalAngry);
            
            return reactionResults;
        }


        // PUT: api/Reactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReaction(string id, Reaction reaction)
        {
            if (id != reaction.Email)
            {
                return BadRequest();
            }

            _context.Entry(reaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactionExists(id))
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

        // POST: api/Reactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reaction>> PostReaction(Reaction reaction)
        {
            _context.Reaction.Add(reaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReactionExists(reaction.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReaction", new { id = reaction.Email }, reaction);
        }

        [HttpPost("UpdateReaction")]
        public async Task<ActionResult<Reaction>> UpdateReaction([FromBody] UpdateReactionsReactionBindingModel model)
        {
            var result = await _context.Reaction.Where(x => x.Email == model.Email && x.NewsId == model.NewsId).SingleOrDefaultAsync();            

            //IF THERES NO DATA THEN CREATE IT
            if (result == null)
            {
                Reaction reaction = new Reaction(model.Email, model.NewsId, model.ReactionLike, model.ReactionWow, model.ReactionSad, model.ReactionAngry);
                _context.Reaction.Add(reaction);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (ReactionExists(reaction.Email))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetReaction", new { id = reaction.Email }, reaction);
            }

            result.ReactionAngry = model.ReactionAngry;
            result.ReactionLike = model.ReactionLike;
            result.ReactionSad = model.ReactionSad;
            result.ReactionWow = model.ReactionWow;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReaction", new { id = result.Email }, result);
        }

        // DELETE: api/Reactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(string id)
        {
            var reaction = await _context.Reaction.FindAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }

            _context.Reaction.Remove(reaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReactionExists(string id)
        {
            return _context.Reaction.Any(e => e.Email == id);
        }
    }
}
