namespace NBA_Stats.Models
{
    public class PlayerModel
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public string height_feet { get; set; }
        public string height_inches { get; set; }
        public string weight_pounds { get; set; }
        public string headshot_url { get; set; }
        public string season { get; set; }

        public TeamModel team { get; set; }
        public SeasonAvgModel stats { get; set; }
    }

    public class PlayerListModel
    {
        public PlayerListModel()
        {
            players = new List<PlayerModel>();
        }
        public List<PlayerModel> players { get; set; }
    }

    public class PlayerSearchModel
    {

        public PlayerSearchModel()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public bool includeScripts { get; set; }
        public bool isRemovable { get; set; }
        public string GUID { get; set; }

    }

    public class PlayerHeadshotModel
    {
        public string fullName { get; set; }
        public int id { get; set; }
        public int personID { get; set; }
    }

    public class TeamModel
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string abbreviation { get; set; }
        public string logo_url
        {
            get
            {
                return $"http://i.cdn.turner.com/nba/nba/.element/img/1.0/teamsites/logos/teamlogos_500x500/{abbreviation.ToLower()}.png";
            }
        }
    }


    public class SeasonAvgModel
    {
        public int player_id { get; set; }
        public int season { get; set; }
        public int games_played { get; set; }
        public decimal fga { get; set; }
        public decimal fgm { get; set; }
        public decimal fg3m { get; set; }
        public decimal pts { get; set; }
        public decimal reb { get; set; }
        public decimal ast { get; set; }
        public decimal stl { get; set; }
        public decimal blk { get; set; }
        public decimal fg_pct { get; set; }
        public decimal eff_fg_pct
        {
            get
            {
                return Decimal.Round(((fgm * games_played) + ((decimal)0.5 * fg3m)) / (games_played * fga), 3);
            }
        }
    }

    public class SeasonAvgDataModel
    {
        public List<SeasonAvgModel> data { get; set; }
    }
}
