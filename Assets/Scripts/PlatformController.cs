using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformController : MonoBehaviour
{
    public GameObject player;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


    private Rigidbody2D platformRB;
    private Rigidbody2D playerRB;
    private SpriteRenderer platformSpriteRenderer;

    private Color[] colors;
    private int colorIndex;
    private bool onMovingPlatform;

    private float alpha = 0.5f;
    Color red, green, blue, yellow, black;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.5f;
        red = new Color(255, 0, 0, alpha);
        green = new Color(0, 255, 0, alpha);
        blue = new Color(0, 0, 255, alpha);
        yellow = new Color(255, 255, 0, alpha);
        black = new Color(0, 0, 0, 1);

        if (gameObject.CompareTag("MovingPlatform"))
        {
            playerRB = player.GetComponent<Rigidbody2D>();
        }
        platformRB = GetComponent<Rigidbody2D>();

        colorIndex = 0;
        platformSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        colors = new Color[] { red, green, yellow, blue };

        onMovingPlatform = false;

        InvokeRepeating("ChangeColor", 0.0f, 2.0f);
    }

    // Update is called once per a frame
    void LateUpdate()
    {

        if (platformSpriteRenderer.color.CompareRGB(extractColorOfNextBottle()))
        {
            gameObject.layer = 3;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            gameObject.layer = 0;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }

        if (onMovingPlatform)
        {
            if (playerRB.velocity.x == 0)
            {
                playerRB.velocity = new Vector2(platformRB.velocity.x, playerRB.velocity.y);
            }
        }

    }

    void ChangeColor()
    {
        colorIndex = (colorIndex) % colors.Length;
        platformSpriteRenderer.color = colors[colorIndex];
        colorIndex++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");


        if (collision.gameObject.CompareTag("Player"))
        {

            onMovingPlatform = true;
            if (platformSpriteRenderer.color.CompareRGB(extractColorOfNextBottle()))
            {
                alpha = 1.0f;
                platformSpriteRenderer.color = extractColorOfNextBottle();

                gameObject.GetComponent<Collider2D>().isTrigger = false;

                CancelInvoke();
            }


            if (gameObject.CompareTag("MovingPlatform"))
            {
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited");


        if (collision.gameObject.CompareTag("Player") && collision.gameObject.activeSelf)
        {
            onMovingPlatform = false;
            if (platformSpriteRenderer.color.CompareRGB(extractColorOfNextBottle()))
            {
                Color platformColor = platformSpriteRenderer.color;
                platformColor.a = 1f;
                platformSpriteRenderer.color = platformColor;

                Start();
            }
        }

        if (gameObject.CompareTag("MovingPlatform"))
        {
        }
    }

    private Color extractColorOfNextBottle()
    {
        TMP_Text nextBottle = GameObject.Find("NextBottle").GetComponent<TMP_Text>();

        switch (nextBottle.text)
        {
            case "Red":
                return new Color(255, 0, 0, alpha); ;

            case "Blue":
                return new Color(0, 0, 255, alpha);

            case "Green":
                return new Color(0, 0, 255, alpha);

            case "Yellow":
                return new Color(255, 255, 0, alpha);

            default:
                return black;
        }
    }
}

