using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entities
{
    [System.Serializable]
    public class Region
    {
        public int id;
        public string name;
        public string platform;
        public string host;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Rank
    {
        public int id;
        public string slug;
        public string group_name;
        public int group_level;
        public int level;
        public string icon;
        public string border;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class GameType
    {
        public int id;
        public int game_id;
        public string slug;
        public int nb_players;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class League
    {
        public int id;
        public int summoner_id;
        public int game_type_id;
        public int rank_id;
        public string league_id;
        public string name;
        public int points;
        public int wins;
        public int losses;
        public int hot_streak;
        public int veteran;
        public int inactive;
        public int fresh_blood;
        public string created_at;
        public string updated_at;
        public Rank rank;
        public GameType game_type;
    }

    [System.Serializable]
    public class LolSummoner
    {
        public int id;
        public int user_id;
        public int region_id;
        public int account_id;
        public int profile_icon_id;
        public string name;
        public int level;
        public string revision_date;
        public string created_at;
        public string updated_at;
        public Region region;
        public List<League> leagues;
    }

    [System.Serializable]
    public class User
    {
        public int id;
        public string name;
        public string slug;
        public string email;
        public int status;
        public object last_notification_check_at;
        public string created_at;
        public string updated_at;
        public LolSummoner lol_summoner;
        public List<object> roles;
        public List<object> permissions;
    }

    [System.Serializable]
    public class RootObjectUser
    {
        public User user;
        public List<object> roles;
        public List<object> permissions;
        public bool rememberMe;
        public List<object> friendships;
        public List<object> readings;

    }

    
}
