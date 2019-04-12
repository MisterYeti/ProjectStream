using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    

    public string cardName = "", streamerName, championName, role;
    public int cardId, visionValue, attackDamagesValue, mentalValue, tankynessValue, magicDamagesValue;
    public bool isPosseded = false;

    [SerializeField]
    GameObject buyButton;

    [SerializeField]
    TextMeshProUGUI txtCardName, txtStreamerName, txtChampionName, txtAttackDamagesValue, txtMagicDamagesValue, txtTankynessValue, txtMentalValue, txtVisionValue;

    [SerializeField]
    Image imageRole;

    [SerializeField]
    AddCard addCard;


    public Card(CardGame.Card card)
    {
        cardName = card.name;
        streamerName = card.streamer.name;
        championName = card.champion.name;
        role = card.role.name;

        cardId = card.id;
        visionValue = int.Parse(card.caracteristics[0].value);
        attackDamagesValue = int.Parse(card.caracteristics[1].value);
        mentalValue = int.Parse(card.caracteristics[2].value);
        tankynessValue = int.Parse(card.caracteristics[3].value);
        magicDamagesValue = int.Parse(card.caracteristics[4].value);

        foreach (CardGame.Player p in card.players)
        {
            if (p.id == DataManager.Instance.RootUser.user.id)
            {
                isPosseded = true;
            }
        }
    }


    void Start()
    {
        

        if (cardName != "")
        {
            txtCardName.text = cardName;
            txtStreamerName.text = streamerName;
            txtChampionName.text = championName;
       
            txtAttackDamagesValue.text = attackDamagesValue.ToString();
            txtMagicDamagesValue.text = magicDamagesValue.ToString();
            txtTankynessValue.text = tankynessValue.ToString();
            txtMentalValue.text = mentalValue.ToString();
            txtVisionValue.text = visionValue.ToString();

            switch (role)
            {
                case "Mid":
                    imageRole.sprite = Resources.Load<Sprite>("Roles/Mid");
                    break;
                case "Top":
                    imageRole.sprite = Resources.Load<Sprite>("Roles/Top");
                    break;
                case "Jungle":
                    imageRole.sprite = Resources.Load<Sprite>("Roles/Jungle");
                    break;
                case "Adc":
                    imageRole.sprite = Resources.Load<Sprite>("Roles/ADC");
                    break;
                case "Support":
                    imageRole.sprite = Resources.Load<Sprite>("Roles/Support");
                    break;
            }

            if (!isPosseded)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0, 255);               
            }
            else
            {
                buyButton.SetActive(false);
            }
            
        }
    }

    public void AddCard()
    {
        addCard.AddCardToPlayer(this.cardId);
    }


    
}
