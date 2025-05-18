using System.Collections.Generic;

namespace BEBChampionship.Models
{
    public class ScoreViewModel
    {
        public Score Score { get; set; }
        public List<Player> Players { get; set; }
        public List<GolfCourse> Courses { get; set; }
    }
}
