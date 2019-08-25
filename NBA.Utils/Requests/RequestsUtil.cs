using NBA.Utils.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NBA.Utils.Requests
{
    public static class RequestsUtil
    {
        [HttpPost]
        public static async Task<Team> GetTeam(int teamId)
        {
            var endpoint = $"https://free-nba.p.rapidapi.com/teams/{teamId}";

            var content = await BuildRequests.Build(endpoint);

            return JsonConvert.DeserializeObject<Team>(content.ToString());
        }

        private static async Task<AllPlayersResponse> GetAllPlayers(int page, int per_page)
        {
            var endpoint = $"https://free-nba.p.rapidapi.com/players?page={page}&per_page={per_page}";

            var content = await BuildRequests.Build(endpoint);
            var player = JsonConvert.DeserializeObject<AllPlayersResponse>(content.ToString());

            //List<Player> orderedPlayers = player.Data.OrderBy(x => x.Height_feet + x.Height_inches).ToList();
            //return new AllPlayersResponse { Data = orderedPlayers, Meta = player.Meta };
            return player;
        }

        public static async Task<AllMatchesResponse> GetAllMatches(int page, int per_page)
        {
            var endpoint = $"https://free-nba.p.rapidapi.com/games?Seasons=2018%2C2017&page={page}&per_page={per_page}";
            var content = await BuildRequests.Build(endpoint);

            var jObj = JsonConvert.DeserializeObject<AllMatchesResponse>(content.ToString());

            return jObj;
        }


        public static async Task<Dictionary<int, Player>> GetPlayers()
        {
            AllPlayersResponse players = new AllPlayersResponse();
            int currPage = 0;
            Dictionary<int, Player> playersList = new Dictionary<int, Player>();

            do
            {
                try
                {
                    players = await GetAllPlayers(currPage, 100);
                }
                catch (Exception ew)
                { throw new Exception(ew.Message); }

                foreach (var player in players.Data)
                //Parallel.ForEach(players.Data, player =>
                {
                    if (!playersList.ContainsKey(player.Id))
                    {
                        if (player != null)
                        {
                            playersList.Add(player.Id, player);
                        }
                    }
                }
                ++currPage;
            }
            while (currPage <= players.Meta.Total_pages && players.Meta.Next_page != null);

            return playersList.OrderBy(x=>x.Value.Height_feet).ToDictionary(y=>y.Key, y=>y.Value);
        }

    }
}
