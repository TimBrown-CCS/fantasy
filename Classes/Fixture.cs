namespace fantasy.Classes
{
    public class Fixture
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public Fixture(string Day, string Month, string Year, string HomeTeam, string AwayTeam)
        {
            this.Day = Day;
            this.Month = Month;
            this.Year = Year;
            this.HomeTeam = HomeTeam;
            this.AwayTeam = AwayTeam;
        }
    }
}