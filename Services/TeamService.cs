using NBA_Stats.Models;
using System.Linq;

namespace NBA_Stats.Services
{
    public class TeamService
    {
        public List<Team_Record_Date> Get_Team_Record(int teamID, int season = 2021)
        {
            GamesResultModel games = new GamesResultModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.balldontlie.io/api/v1/");
                //HTTP GET
                var responseTask = client.GetAsync($"games?per_page=100&seasons[]={season}&team_ids[]={teamID}&postseason=false");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GamesResultModel>();
                    readTask.Wait();

                    games = readTask.Result;
                }
                else //web api sent error response 
                {

                }
            }


            List<Team_Record_Date> recordHistory = new List<Team_Record_Date>();

            DateTime seasonStart = new DateTime(2021, 10, 18);
            DateTime seasonEnd = new DateTime(2022, 4, 11);
            DateTime today = DateTime.Now;
            DateTime endDt = (today < seasonEnd ? today : seasonEnd);

            int winsCount = 0;
            int lossesCount = 0;

            for (DateTime day = seasonStart; day <= endDt; day = day.AddDays(1))
            {
                Team_Record_Date record = new Team_Record_Date();

                var game = games.Data.FirstOrDefault(d => d.date == day);

                if (game!= null)
                {
                    
                    if (Team_Did_Win(teamID, game))
                    {
                        winsCount++;
                    }
                    else
                    {
                        lossesCount++;
                    }
                    
                }

                record.Date = day;
                record.date_2 = day.ToShortDateString();
                record.Wins = winsCount;
                record.Losses = lossesCount;

                recordHistory.Add(record);

            }

            return recordHistory;
        }


        private bool Team_Did_Win(int teamID, GameModel game)
        {
            if (teamID == game.home_team.id)
            {
                return game.home_team_score > game.visitor_team_score;
            }
            else
            {
                return game.visitor_team_score > game.home_team_score;
            }
        }

        public TeamModel Get_Team(int id)
        {
            TeamModel team = new TeamModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.balldontlie.io/api/v1/");
                //HTTP GET
                var responseTask = client.GetAsync($"teams/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TeamModel>();
                    readTask.Wait();

                    team = readTask.Result;
                }
                else //web api sent error response 
                {

                }
            }

            return team;
        }
    }
}
