using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        public async Task<ActionResult<IEnumerable<ScoreDisplayDTO>>> GetPublicScores()
        {
            IEnumerable<Score> scores = await _context.Score.ToListAsync();
            return Ok(scores.Select(c => new ScoreDisplayDTO(c)));

        }

        // GET: api/Scores/5
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Score>> GetMyScores()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (user == null) return Unauthorized();
           

            var scores = await _context.Score
                .Where(s => s.Pseudo == user)
                .OrderByDescending(s => s.Date)
                .ToListAsync();

            return Ok(scores);
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(int id)
        {
            // Recevoir l'id du score à modifier
            // trouver le score dans la BD
            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            // S'assurer que celui qui envoie la requête est le propriétaire du score
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            //if (user == null || ) return Unauthorized();
            

            // Modifier le score
            score.IsPublic = !score.IsPublic;
            // Sauvegarder la BD
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

            // youpi

            
            
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
                Pseudo = user
               
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
