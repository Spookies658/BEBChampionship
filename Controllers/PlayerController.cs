using BEBChampionship.Data;
using BEBChampionship.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;



public class PlayerController : Controller
{
    private readonly BEBContext _context;  // ✅ Use your actual DbContext name

    public PlayerController(BEBContext context) // ✅ Update constructor
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var players = await _context.Players.ToListAsync(); // Get all players from the database
        ViewBag.Players = players; // Pass players to the layout view for navbar
        return View(players); // Optionally return the list of players to the view
    }

    public async Task<IActionResult> Stats(int playerId)
    {
        if (playerId == 0) return NotFound();

        var player = await _context.Players.FindAsync(playerId);
        if (player == null) return NotFound();

        ViewBag.PlayerName = player.Name;

        var playerStats = await _context.Scores
            .Where(s => s.PlayerId == playerId)
            .Select(s => new PlayerStatsViewModel
            {
                Id = s.Id,
                GolfCourse = s.GolfCourse.Name,
                Score = s.ScoreValue,
                NetScore = s.NetScore,
                FIR = s.FIR,
                GIR = s.GIR,
                PuttsPerRound = s.PuttsPerRound
            })
            .OrderByDescending(s => s.GolfCourse)
            .ToListAsync();

        return View(playerStats);
    }

    public async Task<IActionResult> Edit(int statId)
    {
        // Find the stat entry by statId
        var stat = await _context.Scores
            .Where(s => s.Id == statId)
            .FirstOrDefaultAsync();

        if (stat == null)
        {
            return NotFound();
        }

        // Return the stat entry to the view
        var model = new EditPlayerStatsViewModel
        {
            Id = stat.Id,
            GolfCourse = stat.GolfCourse.Name,
            Score = stat.ScoreValue,
            NetScore = stat.NetScore,
            FIR = stat.FIR,
            GIR = stat.GIR,
            PuttsPerRound = stat.PuttsPerRound
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPlayerStatsViewModel model)
    {
        if (ModelState.IsValid)
        {
            var stat = await _context.Scores.FindAsync(model.Id);

            if (stat == null)
            {
                return NotFound();
            }

            
            stat.ScoreValue = model.Score;
            stat.NetScore = model.NetScore;
            stat.FIR = model.FIR;
            stat.GIR = model.GIR;
            stat.PuttsPerRound = model.PuttsPerRound;
            

            await _context.SaveChangesAsync();

            return RedirectToAction("Stats", new { playerId = model.PlayerId });
        }

        return View(model);
    }

    public async Task<IActionResult> Delete(int statId)
    {
        var stat = await _context.Scores.FindAsync(statId);

        if (stat == null)
        {
            return NotFound();
        }

        // Remove the stat entry from the database
        _context.Scores.Remove(stat);
        await _context.SaveChangesAsync();

        return RedirectToAction("Stats", new { playerId = stat.PlayerId });
    }


}
