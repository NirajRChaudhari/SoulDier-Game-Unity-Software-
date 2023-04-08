using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private Rigidbody2D fallingPlatformRB;
    private SpriteRenderer fallingPlatformSpriteRenderer;
    private Color[] colors;
    private int currentColorIndex;

    public static bool stopColorChange = false, playerKeyPressed = false;
    private SpriteRenderer playerSpriteRenderer;
    private float initialXposPlatform, initialYposPlatform;

    private Transform lowestFallingPlatformPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        playerKeyPressed = false;
        currentColorIndex = 0;
        colors = new Color[] { Color.red, Color.yellow, Color.blue };

        fallingPlatformSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        fallingPlatformRB = gameObject.GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        fallingPlatformRB.gravityScale = 0.0f;
        
        initialXposPlatform = transform.position.x;
        initialYposPlatform = transform.position.y;

        lowestFallingPlatformPoint = transform.parent.GetChild(2).GetComponent<Transform>();

        InvokeRepeating("ChangeColor", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowestFallingPlatformPoint.position.y )
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.25f, transform.localScale.y, transform.localScale.z);

            transform.position = new Vector3(initialXposPlatform, initialYposPlatform, 0);
            fallingPlatformRB.gravityScale = 0.0f;
            fallingPlatformRB.bodyType = RigidbodyType2D.Static;
            playerKeyPressed = false;
        }

        if (FallingPlatKeyboardController.playerInKeyboardZone && Input.GetKeyDown(KeyCode.C) && !playerKeyPressed)
        {
            if (fallingPlatformSpriteRenderer.color.r == playerSpriteRenderer.color.r
                && fallingPlatformSpriteRenderer.color.g == playerSpriteRenderer.color.g
                && fallingPlatformSpriteRenderer.color.b == playerSpriteRenderer.color.b)
            {
                CancelInvoke();
                fallingPlatformRB.gravityScale = 1.5f;
                transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y, transform.localScale.z);

            }
            else
            {
                fallingPlatformRB.gravityScale = 1.5f;
                transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y, transform.localScale.z);
            }

            fallingPlatformRB.bodyType = RigidbodyType2D.Dynamic;
            playerKeyPressed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
            fallingPlatformRB.bodyType = RigidbodyType2D.Static;

    }
    void ChangeColor()
    {
        fallingPlatformSpriteRenderer.color = colors[currentColorIndex];
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
    }
}
