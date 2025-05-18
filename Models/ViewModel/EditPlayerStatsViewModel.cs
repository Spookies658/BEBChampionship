namespace BEBChampionship.Models.ViewModel
{
    public class EditPlayerStatsViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string GolfCourse { get; set; }
        public int Score { get; set; }
        public int NetScore { get; set; }
        public double FIR { get; set; }
        public double GIR { get; set; }
        public double PuttsPerRound { get; set; }
    }

}
