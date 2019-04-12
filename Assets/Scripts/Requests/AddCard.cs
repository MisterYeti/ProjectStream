using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCard : MonoBehaviour {


    // Allows to add a card to the player after he bought it


    public void AddCardToPlayer(int cardId)
    {
        StartCoroutine(AddCardToPlayerCoroutine(cardId, DataManager.Instance.RootUser.user.id));
    }

    IEnumerator AddCardToPlayerCoroutine(int cardId, int playerId)
    {

        WWWForm form = new WWWForm();


        yield return Request.RequestCoroutinePost<CardGame.Card>(
            "https://learn2play.fr/api/cardgame/register-card/" + cardId.ToString() + "/" + playerId.ToString() + "/",
            form,
            null,
            null,
            DisableLoadingLogo,
            ReloadCards
            );
    }

    void UpdateCardLists()
    {

        FindObjectOfType<CollectionCards>().ResetPage();
        GameManager.Instance.SetLoadingLogo(false);

    }

    void DisableLoadingLogo()
    {
        GameManager.Instance.SetLoadingLogo(true);
    }


    void GetCards(CardGame.RootObjectCard cards)
    {
        DataManager.Instance.CardsAll.Clear();
        DataManager.Instance.CardsPosseded.Clear();
        DataManager.Instance.CardsNonPosseded.Clear();

        bool cardPosseded = false;
        foreach (var c in cards.data)
        {
            DataManager.Instance.CardsAll.Add(c);

            foreach (CardGame.Player player in c.players)
            {
                if (player.id == DataManager.Instance.RootUser.user.id)
                {
                    cardPosseded = true;
                    if (!DataManager.Instance.CardsPosseded.Contains(c))
                        DataManager.Instance.CardsPosseded.Add(c);
                }
            }
            if (!cardPosseded)
            {
                DataManager.Instance.CardsNonPosseded.Add(c);
            }

            cardPosseded = false;
        }
    }

    void ReloadCards()
    {
        StartCoroutine(LoadCardsCoroutine());
    }

    IEnumerator LoadCardsCoroutine()
    {

        yield return Request.RequestCoroutineGet<CardGame.RootObjectCard>(
           "https://learn2play.fr/api/cardgame/card",
           GetCards,
           null,
           null,
           UpdateCardLists
           );

    }
}
