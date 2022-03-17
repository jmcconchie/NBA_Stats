using Microsoft.AspNetCore.Mvc;
using NBA_Stats.Models;
using NBA_Stats.Services;

namespace NBA_Stats.Controllers
{
    public class TeamController : BaseController
    {
        private readonly TeamService ts = new TeamService();
        public IActionResult Index()
        {

            return View();
        }

        public ActionResult Team_Details(int id)
        {
            TeamModel team = new TeamModel();
            team = ts.Get_Team(id);

            return View(team);
        }

        public JsonResult Team_Record_History(int id, int season = 2021)
        {
            TeamModel team = new TeamModel();
            team.id = id;

            team.Team_Record_History = ts.Get_Team_Record(team.id, season);

            return Json(team.Team_Record_History);
        }
    }
}
