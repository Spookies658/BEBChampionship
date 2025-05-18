namespace BEBChampionship.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int GolfCourseId { get; set; }
        public GolfCourse GolfCourse { get; set; }

        public int ScoreValue { get; set; }
        public int NetScore { get; set; }
        public double FIR { get; set; } // Fairways in Regulation
        public double GIR { get; set; } // Greens in Regulation
        public double PuttsPerRound { get; set; }
    }
}
