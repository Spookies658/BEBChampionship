using BEBChampionship.Data;
using BEBChampionship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class LeaderboardController : Controller
{
    private readonly BEBContext _context;

    public LeaderboardController(BEBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var leaderboard = _context.Players
            .Select(p => new
            {
                Player = p,
                TotalNetScore = _context.Scores.Where(s => s.PlayerId == p.Id).Sum(s => s.NetScore),
                TotalScore = _context.Scores.Where(s => s.PlayerId == p.Id).Sum(s => s.ScoreValue),
                CoursesPlayed = _context.Scores.Where(s => s.PlayerId == p.Id).Count()
            })
            .ToList()
            .Select(l => new Leaderboard
            {
                Player = l.Player,
                TotalNetScore = l.TotalNetScore,
                TotalScore = l.TotalScore,
                CoursesPlayed = l.CoursesPlayed,
                TotalPoints = CalculatePoints(l.Player.Id)
            })
            .OrderByDescending(l => l.TotalPoints)
            .ThenBy(l => l.TotalNetScore)
            .ToList();

        return View(leaderboard);
    }

    private int CalculatePoints(int playerId)
    {
        int totalPoints = 0;

        // Define special courses and their custom point system
        var specialCourses = new List<string> { "Bellville", "Atlantic Beach", "East London", "Royal Cape", "West Bank" };

        // Get all unique golf courses that have scores
        var golfCourses = _context.Scores.Select(s => s.GolfCourseId).Distinct().ToList();

        foreach (var courseId in golfCourses)
        {
            // Fetch course details
            var course = _context.GolfCourses.FirstOrDefault(c => c.Id == courseId);
            string courseName = course?.Name ?? "";

            bool isSpecialCourse = specialCourses.Contains(courseName);

            var scoresForCourse = _context.Scores
                .Where(s => s.GolfCourseId == courseId)
                .OrderBy(s => s.NetScore) // Rank players based on NetScore (lower is better)
                .ToList();

            int rank = 1;

            foreach (var score in scoresForCourse)
            {
                if (score.PlayerId == playerId)
                {
                    if (isSpecialCourse)
                    {
                        // Special Course Point System
                        if (rank == 1) totalPoints += 6;
                        else if (rank == 2) totalPoints += 3;
                        else if (rank == 3) totalPoints += 1;
                    }
                    else
                    {
                        // Default Point System
                        if (rank == 1) totalPoints += 3;
                        else if (rank == 2) totalPoints += 1;
                    }
                }
                rank++;
            }
        }

        return totalPoints;
    }


}
