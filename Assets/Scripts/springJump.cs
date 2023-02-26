using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springJump : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collided");
            GameObject.Find("Player").transform.localScale = new Vector2(1, 0.8f);
        }
    }
}
