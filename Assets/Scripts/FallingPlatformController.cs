using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private Rigidbody2D fallingPlatformRB;
    private SpriteRenderer platformSpriteRenderer;
    private Color[] colors;
    private int currentColorIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentColorIndex = 0;
        colors = new Color[] { Color.red, Color.yellow, Color.blue };
        platformSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        InvokeRepeating("ChangeColor", 0.0f, 0.5f);
        fallingPlatformRB = gameObject.GetComponent<Rigidbody2D>();
        fallingPlatformRB.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeColor()
    {
        platformSpriteRenderer.color = colors[currentColorIndex];
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
    }
}
