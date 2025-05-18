namespace BEBChampionship.Models
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int TotalNetScore { get; set; }
        public int TotalScore { get; set; }
        public int TotalPoints { get; set; }
        public int CoursesPlayed { get; set; }
    }
}
