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
                messageBox.text = "Jump on the Spring to gain more height";

                Invoke(nameof(ResetMessageBox), 4f);
            }
        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

}
