using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePlayer : MonoBehaviour
{
    public TMP_Text messageBox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         float scaleX = collision.gameObject.transform.localScale.x;
    //         float scaleY = collision.gameObject.transform.localScale.y;

    //         collision.gameObject.transform.localScale = new Vector2(scaleX * 0.8f, scaleY * 0.8f);
    //         PlayerHealthController.instance.DealDamage();
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

// SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            
            if (gameObject.CompareTag("Spike"))
            {
                PlayerHealthController.instance.DealDamage("Spike");
                                // ob.Send("Spike");
                                // Debug.Log("Spike");
                // PlayerHealthController.instance.DealDamage();
            }
            else if (gameObject.CompareTag("Rotating Saw"))
            {
                PlayerHealthController.instance.DealDamage("Rotating Saw");
                                // ob.Send("Rotating Saw");
                                // Debug.Log("Rotating Saw");
                // PlayerHealthController.instance.DealDamage();
            }
        }
    }
}
