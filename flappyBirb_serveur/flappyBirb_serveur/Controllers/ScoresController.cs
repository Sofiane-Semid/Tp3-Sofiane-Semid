using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;
using flappyBirb_serveur.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flappyBirb_serveur.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly flappyBirb_serveurContext _context;
        private readonly UserManager<User> _userManager;

        public ScoresController(flappyBirb_serveurContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScores()
        {
            return await _context.Score.ToListAsync();
        }

        // GET: api/Scores/5
        [HttpGet("MyScores")]
        [Authorize]
        public async Task<ActionResult<Score>> GetMyScores(int id)
        {
            var score = await _context.Score.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> ChangeScoreVisibility(int id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
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

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Score>> PostScore(ScoreDTO scoreDTO)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (user == null) return Unauthorized(); // Non authentifié ou token invalide

            // ✅ Le lien entre l'utilisateur est concrétisé par cette propriété de navigation !
            Score score = new Score
            {
                Id = 0,
                ScoreValue = scoreDTO.ScoreValue,
                TimeInSeconds = scoreDTO.TimeInSeconds,
                Date = DateTime.Now,
                IsPublic = false,
               
            };

            _context.Score.Add(score);
            await _context.SaveChangesAsync();

            return score;
        }

       

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.Id == id);
        }
    }
}
