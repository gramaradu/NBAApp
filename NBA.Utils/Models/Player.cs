using System;

namespace NBA.Utils.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public int? Height_feet { get; set; }
        public int? Height_inches { get; set; }
        public string Last_name { get; set; }
        public string Position { get; set; }
        public virtual Team Team { get; set; }
        public int? Weight_pounds { get; set; }
    }
}