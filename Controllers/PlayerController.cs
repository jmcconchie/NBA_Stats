using Microsoft.AspNetCore.Mvc;
using NBA_Stats.Models;
using NBA_Stats.Services;

namespace NBA_Stats.Controllers
{
    public class PlayerController : BaseController
    {
        private readonly PlayerService ps = new PlayerService();

        public ActionResult Player_Search()
        {
            return View();
        }

        public JsonResult Get_Random_Player()
        {
            var random = new Random();
            List<PlayerHeadshotModel> playersWithHeadshots = ps.Players_With_Headshots();
            int index = random.Next(playersWithHeadshots.Count);

            return Json(playersWithHeadshots[index]);
        }

        public PartialViewResult Get_Player(int playerID)
        {
            PlayerModel player = ps.Get_Player(playerID);

            return PartialView("_player", player);

        }

        public PartialViewResult Get_Stats(int playerID, int season)
        {
            SeasonAvgModel stats = new SeasonAvgModel();
            stats = ps.Get_Player_Stats(playerID, season);
            return PartialView("_playerStats", stats);
        }

        public ActionResult Player_Comparison(string id)
        {
            return View();
        }

        public JsonResult Get_Player_Comparison(string players, string seasons)
        {
            PlayerListModel mod = new PlayerListModel();

            string[] playersArray = players.Split(',');
            string[] seasonsArray = seasons.Split(',');

            for (int i = 0; i < playersArray.Length; i++)
            {
                PlayerModel player = ps.Get_Player(int.Parse(playersArray[i]));
                player.stats = ps.Get_Player_Stats(int.Parse(playersArray[i]), int.Parse(seasonsArray[i]));
                mod.players.Add(player);

            }

            return Json(mod);
        }
    }
}
