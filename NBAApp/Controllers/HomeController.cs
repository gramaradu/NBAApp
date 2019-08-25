using NBA.App.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using NBA.Utils;
using System.Web.Mvc;
using System.Web.UI;
using NBA.Utils.Models;
using NBA.Utils.Caching;
using NBA.Utils.Images;
using System.Threading.Tasks;
using System.ComponentModel;
using PagedList;
using NBA.App.Models;

namespace NBA.App.Controllers
{
    public class HomeController : Controller
    {
        static bool cacheHasLoaded = false;
        static int index = 0;
        static int playerCount = 0;
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                if ( page < 1)
                {
                    page = 1;
                }

                
                LoadItems(page, 25);
                
                var players = await CacheUtil.GetObjectFromCache("AllPlayersList", 60,
                    NBA.Utils.Requests.RequestsUtil.GetPlayers);

                // ImageProvider.WriteCache(players.Values.ToList());

                var matches = await CacheUtil.GetObjectFromCache($"AllMatchesList_{page}", 60,
                  () => NBA.Utils.Requests.RequestsUtil.GetAllMatches(page, 25));

                return View(matches);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void LoadItems(int page, int pageSize)
        {
            try
            {
                if (cacheHasLoaded)
                {
                    return;
                }
                var players = CacheUtil.GetObjectFromCache("AllPlayersList", 60,
                      NBA.Utils.Requests.RequestsUtil.GetPlayers);
                var model = CacheUtil.GetObjectFromCache("AllMatchesList", 60,
                  () => NBA.Utils.Requests.RequestsUtil.GetAllMatches(page, pageSize));
              
                cacheHasLoaded = true;
            }
            catch (Exception ex)
            {
                ViewBag["message"] = ex.Message;
            }
        }

        private static void LoadPhotos(Dictionary<int, Player> players)
        {
            try
            {
                JObject json = new JObject();
                foreach (var player in players)
                // Parallel.ForEach(players, player =>
                {

                    //CacheUtil.GetObjectFromCache($"{ player.Value.First_Name}_{ player.Value.Last_name}_pic", 60,
                    // () => ImageProvider.GetImage($"{player.Value.First_Name} {player.Value.Last_name} {@player.Value.Team.Full_name}"));
                    //mageProvider.GetImage($"{player.Value.First_Name} {player.Value.Last_name} {@player.Value.Team.Full_name}");
                    if (!json.ContainsKey($"{player.Value.First_Name} {player.Value.Last_name} {@player.Value.Team.Full_name}"))
                    {
                        json = ImageProvider.WriteTODisk($"{player.Value.First_Name} {player.Value.Last_name} {@player.Value.Team.Full_name}");
                    }
                    index = player.Value.Id;
                }
                playerCount += players.Count();
                System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cache.json", json.ToString());

            }
            catch (Exception ex)
            {

            }
        }
    }
}