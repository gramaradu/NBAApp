using System;

namespace NBA.Utils.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Team Home_team { get; set; }
        public int Home_team_score { get; set; }
        public int Period { get; set; }
        public bool Postseason { get; set; }
        public int Season { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
        public Team Visitor_team { get; set; }
        public int Visitor_team_score { get; set; }

    }
}