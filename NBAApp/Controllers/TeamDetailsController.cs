﻿using NBA.Utils.Caching;
using NBA.Utils.Models;
using NBA.Utils.Requests;
using NBA.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NBA.App.Controllers
{
    public class TeamDetailsController : Controller
    {
        public static Dictionary<Player, Team> playerInTeam = new Dictionary<Player, Team>();
       
        public async Task<ActionResult> Index(int teamId = 0)
        {
            try
            {
                if (teamId <= 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                TeamDetails td = new TeamDetails();

                var players = await CacheUtil.GetObjectFromCache("AllPlayersList", 60,
                    RequestsUtil.GetPlayers);
                td.playersList = players.Where(x => x.Value.Team != null && x.Value.Team.Id == teamId)
                    .OrderByDescending(y=>y.Value.Height_inches + y.Value.Height_feet)
                    .ToDictionary(v => v.Key, v => v.Value);

                td.team = await CacheUtil.GetObjectFromCache($"Team_{teamId}", 60,
                    () => RequestsUtil.GetTeam(teamId));

                return View(td);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
