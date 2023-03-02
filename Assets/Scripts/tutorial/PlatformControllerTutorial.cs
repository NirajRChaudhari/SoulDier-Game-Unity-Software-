using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformControllerTutorial : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("MovingPlatform"))
        {
            playerRB = player.GetComponent<Rigidbody2D>();
        }
        platformRB = GetComponent<Rigidbody2D>();

        colorIndex = 0;
        platformSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        colors = new Color[] { Color.red, Color.green, Color.yellow, Color.blue };

        onMovingPlatform = false;

        InvokeRepeating("ChangeColor", 0.0f, 2.0f);
    }

    // Update is called once per a frame
    void Update()
    {
        if (platformSpriteRenderer.color == Color.red)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
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

            if (platformSpriteRenderer.color == Color.red)
            {
                CancelInvoke();
                //platformSpriteRenderer.color = extractColorOfNextBottle();

            }


            if (gameObject.CompareTag("MovingPlatform"))
            {
                onMovingPlatform = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited");


        if (collision.gameObject.CompareTag("Player") && collision.gameObject.activeSelf)
        {
            if (platformSpriteRenderer.color == extractColorOfNextBottle())
            {
                //platformSpriteRenderer.color = Color.red;

                Start();
            }
        }

        if (gameObject.CompareTag("MovingPlatform"))
        {
            onMovingPlatform = false;
        }
    }

    private Color extractColorOfNextBottle()
    {
        // TMP_Text nextBottle = GameObject.Find("NextBottle").GetComponent<TMP_Text>();

        switch ("Red")
        {
            case "Red":
                return Color.red;

            case "Blue":
                return Color.blue;

            case "Green":
                return Color.green;

            case "Yellow":
                return Color.yellow;

            default:
                return Color.black;
        }
    }
}

