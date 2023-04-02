using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public class magneticFieldController : MonoBehaviour
{
    private UnityEngine.U2D.SpriteShapeRenderer magneticFieldSpriteRenderer;

    private Rigidbody2D playerRB;
    public SpriteRenderer playerSpriteRenderer;

    private Color[] colors;
    private int colorIndex;
    private bool isMagnetOn;
    public float magnetForce = 10f;
    private float nonZeroGravity;
    private float alpha = 0.3f;
    Color red, blue, yellow;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.3f;
        red = Color.red;
        red.a = alpha;
        blue = Color.blue;
        blue.a = alpha;
        yellow = Color.yellow;
        yellow.a = alpha;  


        magneticFieldSpriteRenderer = gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        colors = new Color[] { red, yellow, blue };
        colorIndex = 0;

        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        nonZeroGravity = playerRB.gravityScale;
        InvokeRepeating("ChangeColor", 0.0f, 4.0f);
    }

    private void Update()
    {
        if (isMagnetOn)
        {
            playerRB.AddForce(playerRB.transform.right * magnetForce);
        }
    }


    void ChangeColor()
    {
        colorIndex = (colorIndex) % colors.Length;
        magneticFieldSpriteRenderer.color = colors[colorIndex];
        colorIndex++;

    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        Debug.Log(magneticFieldSpriteRenderer.color);
        Debug.Log(playerSpriteRenderer.color);
        if (collision.CompareTag("Player"))
        {
            if (playerSpriteRenderer.color.r == magneticFieldSpriteRenderer.color.r
                && playerSpriteRenderer.color.g == magneticFieldSpriteRenderer.color.g
                && playerSpriteRenderer.color.b == magneticFieldSpriteRenderer.color.b)
            {
                isMagnetOn = true;
                playerRB.gravityScale = 0.0f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerSpriteRenderer.color.r == magneticFieldSpriteRenderer.color.r 
                && playerSpriteRenderer.color.g == magneticFieldSpriteRenderer.color.g 
                && playerSpriteRenderer.color.b == magneticFieldSpriteRenderer.color.b )
            {
                isMagnetOn = true;
                playerRB.gravityScale = 0.0f;
            }
        }
    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMagnetOn = false;
            playerRB.gravityScale = nonZeroGravity;
        }
    }

}

