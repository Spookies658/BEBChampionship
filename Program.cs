using BEBChampionship.Data;
using BEBChampionship.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DB Context
builder.Services.AddDbContext<BEBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BEBContext>();
    if (!context.Players.Any())
    {
        var players = new List<Player>
        {
            new Player { Name = "Brian" },
            new Player { Name = "Ethan" },
            new Player { Name = "Adam" }
        };

        context.Players.AddRange(players);
        context.SaveChanges();
    }

    if (!context.GolfCourses.Any())
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

        context.GolfCourses.AddRange(courses);
        context.SaveChanges();
    }
}


app.Run();
