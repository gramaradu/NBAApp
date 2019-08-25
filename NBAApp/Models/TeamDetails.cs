using NBA.Utils.Models;
using System.Collections.Generic;


namespace NBA.App.Models
{
    public class TeamDetails
    {
        public Dictionary<int,Player> playersList { get; set; }
        public Team team { get; set; }
    }
}