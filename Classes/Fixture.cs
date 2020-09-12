using System;

namespace fantasy.Classes
{
    public class Fixture
    {
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string NameConvert(string name)
        {
            string convertedName;
            if(name == "Manchester United")
            {
                convertedName = "Man Utd";
            } else if (name == "Manchester City")
            {
                convertedName = "Man City";
            } else if (name == "Brighton &amp; Hove Albion")
            {
                convertedName = "Brighton";
            } else if (name == "Leeds United")
            {
                convertedName = "Leeds";
            } else if (name == "Leicester City")
            {
                convertedName = "Leicester";
            } else if (name == "West Bromwich Albion")
            {
                convertedName = "West Brom";
            } else if (name == "Tottenham Hotspur")
            {
                convertedName = "Spurs";
            } else if (name == "Sheffield United")
            {
                convertedName = "Sheffield Utd";
            } else if (name == "Wolverhampton Wanderers")
            {
                convertedName = "Wolves";
            } else if (name == "Newcastle United")
            {
                convertedName = "Newcastle";
            } else if (name == "West Ham United")
            {
                convertedName = "West Ham";
            } else {
                convertedName = name;
            }
            return convertedName;
        }
        public Fixture(DateTime Date, string HomeTeam, string AwayTeam)
        {
            this.Date = Date;
            this.HomeTeam = NameConvert(HomeTeam);
            this.AwayTeam = NameConvert(AwayTeam);
        }
    }
}