using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    private float jumpPower = 20f;
    private float alpha = 0.5f;
    private SpriteRenderer jumpPadSpriteRenderer;
    private Color[] colors;
    private int colorIndex=0;
    Color red, blue, yellow, black;


    private void Start() {
        
        red = Color.red;
        blue = Color.blue;
        yellow = Color.yellow;
        black = new Color(0, 0, 0, 1);
        colors = new Color[] { red, yellow, blue };
        gameObject.layer = 3;
    jumpPadSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    InvokeRepeating("ChangeColor", 0.0f, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer collisionSprite = collision.gameObject.GetComponent<SpriteRenderer>();

        bool condition = (jumpPadSpriteRenderer.color.r == collisionSprite.color.r )&&(jumpPadSpriteRenderer.color.g == collisionSprite.color.g )&&(jumpPadSpriteRenderer.color.b == collisionSprite.color.b );

        if (collision.gameObject.CompareTag("Player") && condition)
        {
            try{
                PlayerController otherInstance = FindObjectOfType<PlayerController>();
                Animator otherAnimator = otherInstance.GetComponent<Animator>();
                otherAnimator.SetBool("doubleJumpAllowed", true);
            }catch(Exception e) {};

            try {
                PlayerControllerTutorial otherInstanceTutorial = FindObjectOfType<PlayerControllerTutorial>();
                Animator otherAnimatorTutorial = otherInstanceTutorial.GetComponent<Animator>();
                otherAnimatorTutorial.SetBool("doubleJumpAllowed", true);
            }catch(Exception e){};

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

        void ChangeColor()
    {
            colorIndex = (colorIndex) % colors.Length;
            jumpPadSpriteRenderer.color = colors[colorIndex];
            colorIndex++;   
    }
}
