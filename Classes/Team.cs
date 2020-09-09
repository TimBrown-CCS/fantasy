namespace fantasy.Classes
{
    public class Team
    {
        // public int form { get; set; }
        public int id { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int loss { get; set; }
        public string name { get; set; }
        public int strength { get; set; }
        public int strength_overall_home { get; set; }
        public int strength_overall_away { get; set; }
        public int strength_attack_home { get; set; }
        public int strength_attack_away { get; set; }
        public int strength_defence_home { get; set; }
        public int strength_defence_away { get; set; }
    }
}