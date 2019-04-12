using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



    public static GameManager Instance = null;

    [SerializeField]
    GameObject LoadingTime;
    Loader loader;  
    public Loader Loader
    {
        get { return loader; }
    }


    public void SetLoadingLogo(bool activate)
    {
        LoadingTime.SetActive(activate);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;           
            loader = FindObjectOfType<Loader>();
        }
        else
        {
            Destroy(gameObject);
        }      
    }

    



}
