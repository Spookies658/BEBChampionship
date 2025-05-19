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
        var specialCourses = new List<string> { "Bellville", "Atlantic Beach", "East London", "Royal Cape", "West Bank" };

        var leaderboard = _context.Players
            .Select(p => new
            {
                Player = p,
                Scores = _context.Scores.Where(s => s.PlayerId == p.Id).ToList()
            })
            .ToList()
            .Select(l =>
            {
                int wins = 0;
                int majorWins = 0;

                var golfCourses = _context.Scores.Select(s => s.GolfCourseId).Distinct().ToList();

                foreach (var courseId in golfCourses)
                {
                    var course = _context.GolfCourses.FirstOrDefault(c => c.Id == courseId);
                    string courseName = course?.Name ?? "";

                    var scoresForCourse = _context.Scores
                        .Where(s => s.GolfCourseId == courseId)
                        .OrderBy(s => s.NetScore)
                        .ToList();

                    // Check if player is 1st for this course
                    var firstPlaceScore = scoresForCourse.FirstOrDefault();
                    if (firstPlaceScore != null && firstPlaceScore.PlayerId == l.Player.Id)
                    {
                        wins++;
                        if (specialCourses.Contains(courseName))
                        {
                            majorWins++;
                        }
                    }
                }

                return new Leaderboard
                {
                    Player = l.Player,
                    TotalNetScore = l.Scores.Sum(s => s.NetScore),
                    TotalScore = l.Scores.Sum(s => s.ScoreValue),
                    CoursesPlayed = l.Scores.Count,
                    TotalPoints = CalculatePoints(l.Player.Id),

                    AverageNetScore = l.Scores.Any() ? Math.Round(l.Scores.Average(s => s.NetScore), 2) : 0,
                    BestNetScore = l.Scores.Any() ? l.Scores.Min(s => s.NetScore) : 0,
                    WorstNetScore = l.Scores.Any() ? l.Scores.Max(s => s.NetScore) : 0,
                    TotalPutts = l.Scores.Sum(s => s.PuttsPerRound),

                    Wins = wins,
                    MajorWins = majorWins
                };
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
