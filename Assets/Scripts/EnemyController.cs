using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        //Once the game start if leftPoint and rightPoint are still the child of Enemy Frog
        //object they will maintain their mutual position with respect to Enemy Frog object
        //and frog will continue to go to left so once game start remove leftPoint and
        //rightPoint from being child of Enemy Frog object.
        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight) //Move to Right
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);

            spriteRenderer.flipX = true;

            if(transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else  //Move to Left
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);

            spriteRenderer.flipX = false;

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }
}