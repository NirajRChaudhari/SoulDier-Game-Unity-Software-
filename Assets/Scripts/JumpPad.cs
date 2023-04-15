using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpPower = 20f;

    


    private void Start() {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController otherInstance = FindObjectOfType<PlayerController>();

    Animator otherAnimator = otherInstance.GetComponent<Animator>();
            otherAnimator.SetBool("doubleJumpAllowed", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
