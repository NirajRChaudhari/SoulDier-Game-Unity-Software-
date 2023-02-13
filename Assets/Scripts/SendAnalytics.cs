using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAnalytics : MonoBehaviour
{

    public GameObject myObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log(myObject.name);
            Debug.Log(PlayerController.totalTime);
            myObject.SetActive(false);
        }
    }
}
