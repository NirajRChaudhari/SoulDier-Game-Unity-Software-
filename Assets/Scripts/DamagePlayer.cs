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

    private void OnTriggerEnter2D(Collider2D collision)
    {
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            // Debug.Log("Jump Counter: "+jump_counter);
            // Debug.Log("seqlen: "+seq_len);

        if(collision.CompareTag("Player"))
        {

            // if (gameObject.CompareTag("EnemyMonster"))
            // {
            //     ob.Send("Monster");
            //     Debug.Log("Monster");
            //     // PlayerHealthController.instance.DealDamage();
            // }
            if (gameObject.CompareTag("Spike"))
            {
                                ob.Send("Spike");
                                Debug.Log("Spike");
                PlayerHealthController.instance.DealDamage();
            }
            
        }
    }
}
