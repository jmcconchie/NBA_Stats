using NBA_Stats.Models;
using System.Net.Http;


namespace NBA_Stats.Services
{
    public class PlayerService
    {
        public List<PlayerHeadshotModel> Players_With_Headshots()
        {
            List<PlayerHeadshotModel> list = new List<PlayerHeadshotModel>
            {
                new PlayerHeadshotModel
                {
                    fullName = "LeBron James",
                    id = 237,
                    personID = 2544
                },
                new PlayerHeadshotModel
                {
                    fullName = "Stephen Curry",
                    id = 115,
                    personID = 201939
                },
                new PlayerHeadshotModel
                {
                    fullName = "Giannis Antetokounmpo",
                    id = 15,
                    personID = 203507
                },
                new PlayerHeadshotModel
                {
                    fullName = "Kevin Durant",
                    id = 140,
                    personID = 201142
                },
                new PlayerHeadshotModel
                {
                    fullName = "Klay Thompson",
                    id = 443,
                    personID = 202691
                },
                new PlayerHeadshotModel
                {
                    fullName = "Joel Embiid",
                    id = 145,
                    personID = 203954
                },
                new PlayerHeadshotModel
                {
                    fullName = "Luka Doncic",
                    id = 132,
                    personID = 1629029
                },
                new PlayerHeadshotModel
                {
                    fullName = "Trae Young",
                    id = 490,
                    personID = 1629027
                },
                new PlayerHeadshotModel
                {
                    fullName = "Kristaps Porzingis",
                    id = 378,
                    personID = 204001
                },
                new PlayerHeadshotModel
                {
                    fullName = "DeMar DeRozan",
                    id = 125,
                    personID = 201942
                },
                new PlayerHeadshotModel
                {
                    fullName = "Ja Morant",
                    id = 666786,
                    personID = 1629630
                },
                new PlayerHeadshotModel
                {
                    fullName = "Nikola Jokic",
                    id = 246,
                    personID = 203999
                },
                new PlayerHeadshotModel
                {
                    fullName = "Donovan Mitchell",
                    id = 322,
                    personID = 1628378
                },
                new PlayerHeadshotModel
                {
                    fullName = "Jayson Tatum",
                    id = 434,
                    personID = 1628369
                },
                new PlayerHeadshotModel
                {
                    fullName = "Rudy Gobert",
                    id = 176,
                    personID = 203497
                },
                new PlayerHeadshotModel
                {
                    fullName = "Domantas Sabonis",
                    id = 406,
                    personID = 1627734
                },
                new PlayerHeadshotModel
                {
                    fullName = "Clint Capela",
                    id = 83,
                    personID = 203991
                },
            };

            return list;

        }

        public int Get_Random_Player_With_Headshot()
        {
            var random = new Random();
            List<PlayerHeadshotModel> playersWithHeadshots = Players_With_Headshots();
            int index = random.Next(playersWithHeadshots.Count);

            return playersWithHeadshots[index].id;

        }


        public string Get_Headshot_URL(int id)
        {
            List<PlayerHeadshotModel> playersWithHeadshots = Players_With_Headshots();


            PlayerHeadshotModel player = playersWithHeadshots.Find(i => i.id == id);


            if (player != null)
            {
                return $"https://ak-static.cms.nba.com/wp-content/uploads/headshots/nba/latest/260x190/{player.personID}.png";
            }
            else
            {
                return "https://www.clipartmax.com/png/full/35-352900_headshot-silhouette-not-available.png";
            }

        }


        public SeasonAvgModel Get_Player_Stats(int playerID, int season = 2021)
        {
            SeasonAvgModel stats = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.balldontlie.io/api/v1/");
                //HTTP GET
                var responseTask = client.GetAsync($"season_averages?season={season}&player_ids[]={playerID}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SeasonAvgDataModel>();
                    readTask.Wait();

                    if (readTask.Result.data.Count > 0)
                    {
                        stats = readTask.Result.data[0];
                    }

                }
                else //web api sent error response 
                {
                    //log response status here..

                    stats = new SeasonAvgModel();


                }
            }

            return stats;
        }

        public PlayerModel Get_Player(int playerID)
        {
            if (playerID < 0)
            {
                playerID = Get_Random_Player_With_Headshot();
            }

            PlayerModel player = new PlayerModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.balldontlie.io/api/v1/");
                //HTTP GET
                var responseTask = client.GetAsync($"players/{playerID}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PlayerModel>();
                    readTask.Wait();

                    player = readTask.Result;
                }
                else //web api sent error response 
                {

                }
            }

            player.headshot_url = Get_Headshot_URL(player.id);

            return player;
        }
    }
}
