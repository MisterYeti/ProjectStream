using Assets.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Request : MonoBehaviour {


    static Request Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }


    // Execute Post and Get requests by an API URL. 
    // Allows to return data, execute a method before/after the request and in case of failure or successfull request.


    //Post generic method

    public static IEnumerator RequestCoroutinePost<T>(string url, WWWForm form = null, Action<T> getObject = null, Action actionFail = null, Action actionBefore = null, Action actionAfter = null)
    {

        if (actionBefore != null)
            actionBefore();
         

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            if (actionFail != null)
                actionFail();
        }
        else
        {
            string jsonResult =
                        System.Text.Encoding.UTF8.
                        GetString(www.downloadHandler.data);


            if (getObject != null)
            {
                T obj = JsonHelper.GetJsonObject<T>(jsonResult);
                getObject(obj);
            }

            if (actionAfter != null)
                actionAfter();
        }

    }

    //Get generic method

    public static IEnumerator RequestCoroutineGet<T>(string url, Action<T> getObject = null, Action actionFail = null, Action actionBefore = null, Action actionAfter = null)
    {

        if (actionBefore != null)
            actionBefore();


        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            actionFail();
        }
        else
        {
            string jsonResult =
                        System.Text.Encoding.UTF8.
                        GetString(www.downloadHandler.data);
            T obj = JsonHelper.GetJsonObject<T>(jsonResult);
            getObject(obj);

            if (actionAfter != null)
                actionAfter();
        }

    }

}
