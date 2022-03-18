namespace NBA_Stats.Models
{
    public class TeamModel
    {
        public int id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public string conference { get; set; } = string.Empty;
        public string logo_url
        {
            get
            {
                return $"https://i.cdn.turner.com/nba/nba/.element/img/1.0/teamsites/logos/teamlogos_500x500/{abbreviation.ToLower()}.png";
            }
        }

        public List<Team_Record_Date>? Team_Record_History { get; set; }

    }

    public class Team_Record_Date
    {

        public DateTime Date { get; set; }
        public string date_2 { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double Winning_Pct 
        {
            get
            {
                int totalGames = Wins + Losses;
                double winPct = 0.5;
                if (totalGames > 0)
                {
                    winPct = (double)Wins / totalGames;
                }


                return winPct;
            }
        }
    }

    public class GamesResultModel
    {
        public List<GameModel> Data { get; set; } = new List<GameModel>();
        public MetaModel Meta { get; set; } = new MetaModel();
    }

    public class GameModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int home_team_score { get; set; }
        public int visitor_team_score { get; set; }
        public int season { get; set; }
        public int period { get; set; }
        public string status { get; set; }
        public string time { get; set; }
        public bool postseason { get; set; }
        public TeamModel home_team { get; set; } 
        public TeamModel visitor_team { get; set; }
    }

    public class MetaModel
    {
        public int total_pages { get; set; }
        public int current_page { get; set; }
        public int? next_page { get; set; }
        public int per_page { get; set; }
        public int total_count { get; set; }

    }
}
