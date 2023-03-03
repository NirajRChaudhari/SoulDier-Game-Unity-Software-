using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
public class SendAnalytics6 : MonoBehaviour
{

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         // Debug.Log(gameObject.name);
    //         // Debug.Log(PlayerController.totalTime);
    //         Send(gameObject.name, PlayerController.totalTime);
    //         gameObject.SetActive(false);

    //     }
    // }

    private string URL;
    private string game_over_reason;

    private void Awake()
    {
        // _sessionId = DateTime.Now.Ticks;
        URL="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfHxd1Uba9H23_LT9jFj0dkVdYC0cBDTcV0ruo9WcXfqPqGRA/formResponse";
        //Send("dafa", 5.7f);
    }
    public void Send(string power_up_name)
    {
        // Assign variables
        // _sessionId = DateTime.Now.Ticks;
        // Debug.Log(checkpoint_name);
        // Debug.Log(time_taken);
        // _checkpoint_name = checkpoint_name;
        // _time_taken = 120f - time_taken;
        // // _testInt = UnityEngine.Random.Range(0, 101);
        // _testBool = true;
        // _testFloat = UnityEngine.Random.Range(0.0f, 10.0f);
        // Debug.Log(seq_len);
        // Debug.Log(jumps_taken);
        StartCoroutine(Post(power_up_name));
    }
    private IEnumerator Post(string power_up_name)
    {
        // Create the form and enter responses
        // Debug.Log(seq_len);
    // Debug.Log(jumps_taken);
        WWWForm form = new WWWForm();
        // form.AddField("entry.452161030", game_over_reason);
        form.AddField("entry.903320907", power_up_name);
        // form.AddField("entry.1841362125", jumps_taken);
        // form.AddField("entry.244123968", testBool);
        // form.AddField("entry.431586524", testFloat);
        // form.AddField("entry.1421423374", _checkpoint_name);
        // form.AddField("entry.42440326", _time_taken);
        //Debug.Log("Hi");
        // Debug.Log(_checkpoint_name);
        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
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
        }

    }
}