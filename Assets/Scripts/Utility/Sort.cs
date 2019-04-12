using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;


public static class Sort {

    public static List<CardGame.Card> SortByName(ref List<CardGame.Card> listCards, bool? desc = null)
    {

        if (desc != null && desc == true)
        {
            listCards = listCards.OrderByDescending(name => name.name).ToList();
        }
        else
        {
            listCards = listCards.OrderBy(name => name.name).ToList();
        }

        return listCards;
    }





}
