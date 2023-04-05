using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatKeyboardController : MonoBehaviour
{
    public static  bool playerInKeyboardZone = false;

    private Rigidbody2D fallingPlatformRB;
    private SpriteRenderer fallingPlatformSR;

    private SpriteRenderer playerSR;

    // Start is called before the first frame update
    void Start()
    {
        playerInKeyboardZone = false;
        fallingPlatformRB = GameObject.Find("FallingPlatform").GetComponent<Rigidbody2D>();

        fallingPlatformSR = GameObject.Find("FallingPlatform").GetComponent<SpriteRenderer>();
        playerSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        //fallingPlatformController = gameObject.GetComponent<FallingPlatformController>();
    }

    // Update is called once per frame
    private void Update()
    {
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
