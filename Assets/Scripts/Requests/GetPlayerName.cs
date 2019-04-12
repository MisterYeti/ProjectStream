using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPlayerName : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
        text.text = "Connected as : " + DataManager.Instance.RootUser.user.name;

    }
	
	
}
