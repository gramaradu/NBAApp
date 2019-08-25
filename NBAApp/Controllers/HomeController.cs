using System;
using System.Web.Mvc;
using NBA.Utils.Caching;
using System.Threading.Tasks;
using NBA.Utils.Requests;

namespace NBA.App.Controllers
{
    public class HomeController : Controller
    {
        static bool cacheHasLoaded = false;
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                if (page < 1)
                {
                    page = 1;
                }

                LoadItems(page, 25);

                var matches = await CacheUtil.GetObjectFromCache($"AllMatchesList_{page}", 60,
                  () => RequestsUtil.GetAllMatches(page, 25));

                var players = await CacheUtil.GetObjectFromCache("AllPlayersList", 60,
                        RequestsUtil.GetPlayers);
                
                return View(matches);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        private void LoadItems(int page, int pageSize)
        {
            if (cacheHasLoaded)
            {
                return;
            }
            var players = CacheUtil.GetObjectFromCache("AllPlayersList", 60,
                  Utils.Requests.RequestsUtil.GetPlayers);

            var model = CacheUtil.GetObjectFromCache("AllMatchesList", 60,
              () => Utils.Requests.RequestsUtil.GetAllMatches(page, pageSize));

            cacheHasLoaded = true;
        }
    }
}