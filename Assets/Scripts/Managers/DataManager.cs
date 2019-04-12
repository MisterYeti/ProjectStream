using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public class DataManager : MonoBehaviour {


    //Contains all the data about the players, the cards, ...

    public static DataManager Instance;

    public RootObjectUser RootUser = new RootObjectUser();
    public List<CardGame.Card> CardsAll = new List<CardGame.Card>();
    public List<CardGame.Card> CardsPosseded = new List<CardGame.Card>();
    public List<CardGame.Card> CardsNonPosseded = new List<CardGame.Card>();

    


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

    }

    

    public void SortCardsByName(ref List<CardGame.Card> list, bool? desc = null)
    {
        list = Sort.SortByName(ref list, desc);
    }

}
