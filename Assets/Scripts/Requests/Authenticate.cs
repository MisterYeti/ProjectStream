using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Authenticate : MonoBehaviour {

    [SerializeField] InputField inputFieldUserName;
    [SerializeField] InputField inputFieldPassword;
    [SerializeField] Button button;
    [SerializeField] GameObject connexionInvalid;
    [SerializeField] GameObject connexionValid;

    DataManager dataManager;


    // Authentication : Checking if the user name & pass are valid. If yes, get all the data about the cards in our DataManager.


    void Start()
    {
        connexionValid.SetActive(false);
        connexionInvalid.SetActive(false);

        dataManager = DataManager.Instance;
    }

    public void Authentication()
    {
        StartCoroutine(AuthenticateCoroutine());
    }

    IEnumerator AuthenticateCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("identifier", inputFieldUserName.text);
        form.AddField("password", inputFieldPassword.text);

        yield return Request.RequestCoroutinePost<RootObjectUser>(
            "https://learn2play.fr/api/authenticate",
            form,
            GetUser,
            RequestFailed,
            BlockButton,
            LoadCards
            );
    }


    void BlockButton()
    {
        button.interactable = false;
        GameManager.Instance.SetLoadingLogo(true);
    }

    void ConnexionSuccess()
    {
        Display(connexionValid);
        
    }

    void GetUser(RootObjectUser rootUser)
    {
        dataManager.RootUser = rootUser;
    }

    void GetCards(CardGame.RootObjectCard cards)
    {
        DataManager.Instance.CardsAll.Clear();
        DataManager.Instance.CardsPosseded.Clear();
        DataManager.Instance.CardsNonPosseded.Clear();

        bool cardPosseded = false;
        foreach (var c in cards.data)
        {
            dataManager.CardsAll.Add(c);

            foreach (CardGame.Player player in c.players)
            {
                if (player.id == dataManager.RootUser.user.id)
                {
                    cardPosseded = true;
                    if (!dataManager.CardsPosseded.Contains(c))
                        dataManager.CardsPosseded.Add(c);
                }
            }
            if (!cardPosseded)
            {
                dataManager.CardsNonPosseded.Add(c);
            }

            cardPosseded = false;
        }
    }

    void RequestFailed()
    {
        Display(connexionInvalid);
        button.interactable = true;
        GameManager.Instance.SetLoadingLogo(false);
    }

    void LoadCards()
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
           ConnexionSuccess
           );
        
    }


    void Display(GameObject go)
    {
        go.SetActive(true);
        StartCoroutine(ActiveAndDesactive(go));
    }

    IEnumerator ActiveAndDesactive(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(2f);
        go.SetActive(false);


        if (go == connexionValid)
        {
            GameManager.Instance.Loader.LoadScene(1);
            GameManager.Instance.SetLoadingLogo(false);
        }
    }
}
