using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHeadKillController : MonoBehaviour
{

    private SpriteRenderer enemySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemySpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");


        if (collision.gameObject.CompareTag("Player"))
        {

            if (enemySpriteRenderer.color.CompareRGB(collision.gameObject.GetComponent<SpriteRenderer>().color))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

}
