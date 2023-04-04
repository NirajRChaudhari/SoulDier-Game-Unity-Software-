using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatKeyboardController : MonoBehaviour
{
    private bool playerInKeyboardZone = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInKeyboardZone = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerInKeyboardZone == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                //Right color selected
            }
            else
            {
                //Wrong color selected
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInKeyboardZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInKeyboardZone = false;
        }
    }
}
