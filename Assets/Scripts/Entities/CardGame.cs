using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGame
{


    [System.Serializable]
    public class Streamer
    {
        public int id;
        public string name;
        public string slug;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Champion
    {
        public int id;
        public string name;
        public string slug;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Role
    {
        public int id;
        public string name;
        public string slug;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Caracteristic2
    {
        public int id;
        public string name;
        public string slug;
        public string type;
        public object options;
        public string created_at;
        public string updated_at;
    }

    [System.Serializable]
    public class Caracteristic
    {
        public int id;
        public int card_id;
        public int caracteristic_id;
        public string value;
        public string created_at;
        public string updated_at;
        public Caracteristic2 caracteristic;
    }

    [System.Serializable]
    public class Pivot
    {
        public int card_id;
        public int player_id;
    }

    [System.Serializable]
    public class Player
    {
        public int id;
        public string name;
        public string slug;
        public int status;
        public object last_notification_check_at;
        public string created_at;
        public string updated_at;
        public Pivot pivot;
    }

    [System.Serializable]
    public class Card
    {
        public int id;
        public int streamer_id;
        public int champion_id;
        public int role_id;
        public string name;
        public string slug;
        public string created_at;
        public string updated_at;
        public Streamer streamer;
        public Champion champion;
        public Role role;
        public List<Caracteristic> caracteristics;
        public List<Player> players;
    }

    [System.Serializable]
    public class RootObjectCard
    {
        public int current_page;
        public List<Card> data;
        public string first_page_url;
        public int from;
        public int last_page;
        public string last_page_url;
        public object next_page_url;
        public string path;
        public int per_page;
        public object prev_page_url;
        public int to;
        public int total;
    }

    [System.Serializable]
    public class CardList
    {
        public List<RootObjectCard> Cards;
    }

}