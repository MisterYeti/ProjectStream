using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CollectionCards : MonoBehaviour {

    DataManager dataManager;

    public Button nextButton, previousButton;
    public Button possededOnlyButton;

    public GameObject Canvas, CardPrefab;
    private Card cardOfPrefab;

    public List<GameObject> cardsInstanciatedOnPage = new List<GameObject>();

    public int pageActuelle = 1;
    public int nbPage;

    private Vector2[] positions = new Vector2[8];

    List<CardGame.Card> currentCards = new List<CardGame.Card>();

    bool _possededOnly;


	
	void Start ()
    {
        
        dataManager = DataManager.Instance;
        _possededOnly = true;

        nextButton.onClick.AddListener(delegate { TaskWithParameters("Up"); });
        previousButton.onClick.AddListener(delegate { TaskWithParameters("Down"); });
        possededOnlyButton.onClick.AddListener(delegate { TaskWithParameters("PossededOnly"); });

        AssignPositions();

        cardOfPrefab = CardPrefab.GetComponent<Card>();

        InstanciateCards(1,_possededOnly);

    }

    public void ResetPage()
    {
        InstanciateCards(1, _possededOnly);
        pageActuelle = 1;
    }

    private void AssignPositions()
    {
        positions[0] = new Vector2(-350, 150);
        positions[1] = new Vector2(-100, 150);
        positions[2] = new Vector2(150, 150);
        positions[3] = new Vector2(400, 150);

        positions[4] = new Vector2(-350, -150);
        positions[5] = new Vector2(-100, -150);
        positions[6] = new Vector2(150, -150);
        positions[7] = new Vector2(400, -150);
    }

    void TaskWithParameters(string message)
    {
        

        if (message == "Up")
        {
            foreach (GameObject card in cardsInstanciatedOnPage)
            {
                Destroy(card);
            }
            cardsInstanciatedOnPage.Clear();
            pageActuelle++;
            InstanciateCards(pageActuelle, _possededOnly);
        }
        else if (message == "Down")
        {
            if (pageActuelle > 1)
            {
                foreach (GameObject card in cardsInstanciatedOnPage)
                {
                    Destroy(card);
                }
                cardsInstanciatedOnPage.Clear();
                pageActuelle--;
                InstanciateCards(pageActuelle, _possededOnly);
            }
        }
        else if (message == "PossededOnly")
        {
            foreach (GameObject card in cardsInstanciatedOnPage)
            {
                Destroy(card);
            }
            cardsInstanciatedOnPage.Clear();
            _possededOnly = !_possededOnly;
            InstanciateCards(pageActuelle, _possededOnly);
        }

        
    }


    //Instanciate the cards on the scene

    public void InstanciateCards(int page, bool possededOnly )
    {

        foreach (GameObject card in cardsInstanciatedOnPage)
        {
            Destroy(card);
        }

        int index = 0;
        currentCards = new List<CardGame.Card>();

        //By Default, sorted by name
        dataManager.SortCardsByName(ref dataManager.CardsAll);
        dataManager.SortCardsByName(ref dataManager.CardsPosseded);


        //Researches 
        List<CardGame.Card> cards = currentCards;

        if (possededOnly)
            cards = dataManager.CardsPosseded;    
        else
            cards = dataManager.CardsAll;





        //~~~~~~~~
        if (cards.Count > 0)
        {
            foreach (CardGame.Card card in cards.Skip((page - 1) * 8).Take(8))
            {
                Card c = new Card(card);

                cardOfPrefab.cardName = c.cardName;
                cardOfPrefab.streamerName = c.streamerName;
                cardOfPrefab.championName = c.championName;
                cardOfPrefab.role = c.role;

                cardOfPrefab.visionValue = c.visionValue;
                cardOfPrefab.attackDamagesValue = c.attackDamagesValue;
                cardOfPrefab.mentalValue = c.mentalValue;
                cardOfPrefab.tankynessValue = c.tankynessValue;
                cardOfPrefab.magicDamagesValue = c.magicDamagesValue;
                cardOfPrefab.isPosseded = c.isPosseded;
                cardOfPrefab.cardId = c.cardId;

                GameObject cardInstanciated = Instantiate(CardPrefab, Canvas.transform);
                cardInstanciated.GetComponent<RectTransform>().localPosition = positions[index];
                cardsInstanciatedOnPage.Add(cardInstanciated);

                index++;
            }
        }

        previousButton.gameObject.SetActive(!(page==1));

        
    }



   


}
