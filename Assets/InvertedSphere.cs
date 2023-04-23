using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvertedSphere : MonoBehaviour
{
    /*public GameObject player;
    private Rigidbody2D platformRB;
    private Rigidbody2D playerRB;*/

    private SpriteRenderer invertedSphereSpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    private Color[] colors;
    private int colorIndex;
    private float alpha = 0.3f;
    private Color red, blue, yellow, noColor;

    public static bool isSphereActive = false;
    public static bool invertControls = false, inTheSphereZone = false;


    void Start()
    {
        invertedSphereSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //invertedSphereSpriteRenderer.color = new Color(255, 255, 255, 0);
        playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        isSphereActive = false;
        invertControls = false;
        inTheSphereZone = false;

    //gameObject.SetActive(false);

    alpha = 0.5f;
        red = Color.red;
        red.a = alpha;
        blue = Color.blue;
        blue.a = alpha;
        yellow = Color.yellow;
        yellow.a = alpha;
        noColor = Color.white;
        noColor.a = 0;

        colorIndex = 0;
        colors = new Color[] { red, yellow, blue };


        InvokeRepeating("ChangeColor", 0.0f, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sphere collided with" + collision.name);
    }


    private void Update()
    {
        //add level condition here
        if (GameObject.Find("Player").transform.position.x > GameObject.Find("Checkpoint1").transform.position.x
            && GameObject.Find("Player").transform.position.x < GameObject.Find("Checkpoint2").transform.position.x)
        {
            isSphereActive = true;
        }
        else
            isSphereActive = false;
    }


    void ChangeColor()
    {
        //If sphere is active only then we change its color
        if (isSphereActive)
        {
            inTheSphereZone = true;
            colorIndex = (colorIndex) % colors.Length;
            invertedSphereSpriteRenderer.color = colors[colorIndex];
            colorIndex++;

            if (invertedSphereSpriteRenderer.color.r == playerSpriteRenderer.color.r
                && invertedSphereSpriteRenderer.color.g == playerSpriteRenderer.color.g
                && invertedSphereSpriteRenderer.color.b == playerSpriteRenderer.color.b
                )
                invertControls = false;
            else
                invertControls = true;

        }
        else //otherwise we keep it's alpha zero
        {
            inTheSphereZone = false;
            invertedSphereSpriteRenderer.color = noColor;
        }
    }
}

