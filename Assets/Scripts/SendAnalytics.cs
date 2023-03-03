using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class SendAnalytics : MonoBehaviour
{

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        // Debug.Log(gameObject.name);
    //        // Debug.Log(PlayerController.totalTime);
    //        Send(gameObject.name, PlayerController.totalTime);
    //        gameObject.SetActive(false);

    //    }
    //}

    private string URL;
    private long _sessionId;
    private int _testInt;
    private bool _testBool;
    private float _testFloat;
    private string _checkpoint_name;
    private string _level_name;
    private float _time_taken;

    private void Awake()
    {
        _sessionId = DateTime.Now.Ticks;
        URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdP2qjFwOtmyO0RRC9PfndXGuA5s9KFW3yfOC9hJY9l3xIrsA/formResponse";
        //Send("dafa", 5.7f);
    }
    public void Send(string checkpoint_name, float time_taken, string level_name)
    {
        // Assign variables
        _sessionId = DateTime.Now.Ticks;
        Debug.Log(checkpoint_name);
        Debug.Log(time_taken);
        _checkpoint_name = checkpoint_name;
        _time_taken = 120f - time_taken;
        _testInt = UnityEngine.Random.Range(0, 101);
        _testBool = true;
        _testFloat = UnityEngine.Random.Range(0.0f, 10.0f);
        _level_name=level_name;

        StartCoroutine(Post(_sessionId.ToString(), checkpoint_name, _testBool.ToString(), _time_taken.ToString(), _checkpoint_name, _time_taken.ToString(), _level_name));
    }
    private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat, string _checkpoint_name, string _time_taken, string _level_name)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.366340186", sessionID);
        form.AddField("entry.655163041", testInt);
        form.AddField("entry.244123968", testBool);
        form.AddField("entry.431586524", testFloat);
        form.AddField("entry.1421423374", _checkpoint_name);
        form.AddField("entry.42440326", _time_taken);
        form.AddField("entry.1385746409", _level_name);
        
        //Debug.Log("Hi");
        // Debug.Log(_checkpoint_name);
        // Send responses and verify result
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        // {
            www.disposeUploadHandlerOnDispose = true;
             www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(URL);
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }

            www.Dispose();
            // form.Dispose();
        // }

    }
}