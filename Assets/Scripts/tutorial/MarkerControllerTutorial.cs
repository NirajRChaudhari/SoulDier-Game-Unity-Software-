using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkerControllerTutorial : MonoBehaviour
{

    //Public Variables
    public TMP_Text messageBox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter Marker");
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "Markers1")
            {
                Debug.Log("Jump now");
                messageBox.text = "Find & Collect Red Bottle. Only Red Platforms are available to jump";

                Invoke(nameof(ResetMessageBox), 4f);
            }
        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

}
