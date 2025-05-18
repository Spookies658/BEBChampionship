namespace BEBChampionship.Models.ViewModel
{
    public class PlayerStatsViewModel
    {

        public int Id { get; set; }
        public string GolfCourse { get; set; }
        public int Score { get; set; }
        public int NetScore { get; set; }
        public double FIR { get; set; }  // Fairways in Regulation (%)
        public double GIR { get; set; }  // Greens in Regulation (%)
        public double PuttsPerRound { get; set; }
    }
}
