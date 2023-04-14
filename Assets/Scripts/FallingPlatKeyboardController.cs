using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallingPlatKeyboardController : MonoBehaviour
{
    public static bool playerInKeyboardZone = false;

    private Rigidbody2D fallingPlatformRB;
    private SpriteRenderer fallingPlatformSR;
    private TMP_Text messageBox;
    private SpriteRenderer playerSR;

    // Start is called before the first frame update
    void Start()
    {
        playerInKeyboardZone = false;
        FallingPlatformController.playerKeyPressed = false;
        fallingPlatformRB = GameObject.Find("FallingPlatform").GetComponent<Rigidbody2D>();

        fallingPlatformSR = GameObject.Find("FallingPlatform").GetComponent<SpriteRenderer>();
        playerSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        messageBox = GameObject.Find("MessageBox").GetComponent<TMP_Text>();
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
            messageBox.text = "Press C when platform color is same as player color";
            Invoke(nameof(ResetMessageBox), 8f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInKeyboardZone = false;
        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }
}