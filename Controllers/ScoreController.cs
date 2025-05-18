using BEBChampionship.Data;
using BEBChampionship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ScoreController : Controller
{
    private readonly BEBContext _context;

    public ScoreController(BEBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {   
        ViewBag.Players = _context.Players.ToList();
        ViewBag.Courses = _context.GolfCourses.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Submit(Score score)
    {
        if (ModelState.IsValid)
        {
            ViewBag.Players = _context.Players.ToList();
            ViewBag.Courses = _context.GolfCourses.ToList();
            return RedirectToAction("Leaderboard", "Leaderboard");
        }
        _context.Scores.Add(score);
        _context.SaveChanges();
        return View("Index");
    }
}
