using BEBChampionship.Data;
using BEBChampionship.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class GolfCourseController : Controller
{
    private readonly BEBContext _context;

    public GolfCourseController(BEBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var courses = _context.GolfCourses.ToList();
        ViewBag.Courses = courses;
        return View(courses);
    }

    // Seed some predefined golf courses
    public IActionResult SeedCourses()
    {
        if (!_context.GolfCourses.Any())
        {
            var courses = new List<GolfCourse>
            {
                new GolfCourse { Name = "Royal Cape" },
                new GolfCourse { Name = "Rondebosch" },
                new GolfCourse { Name = "KDM" },
                new GolfCourse { Name = "West Lake" },
                new GolfCourse { Name = "Kuilsriver" },
                new GolfCourse { Name = "Bellville" },
                new GolfCourse { Name = "Durbanville" },
                new GolfCourse { Name = "Hazendal" },
                new GolfCourse { Name = "Atlantic Beach" },
                new GolfCourse { Name = "Metropolitan" },
                new GolfCourse { Name = "Langebaan" },
                new GolfCourse { Name = "Stellenbosch" },
                new GolfCourse { Name = "West Bank" },
                new GolfCourse { Name = "East London" },
                new GolfCourse { Name = "Olivewood" },
                new GolfCourse { Name = "Gonubie" },
            };

            _context.GolfCourses.AddRange(courses);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}
