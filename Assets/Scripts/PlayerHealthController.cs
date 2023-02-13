using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;
    public TMP_Text messageBox;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer spriteRenderer;


    //Awake function is called before the start function
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <=  0)
        {
            //messageBox.text = "Time Deducted By 5...";

            currentHealth--;

            if (currentHealth <= 0)
            {
                messageBox.text = "GAME OVER";

                gameObject.SetActive(false);
                currentHealth = 0;
                Invoke(nameof(restartLevel), 3f);

            }
            else
            {
                //messageBox.text = "Time Deducted By 10...";

                invincibleCounter = invincibleLength;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
	// 	Debug.Log("Hitting it man $$$$$$$$$$$$$" + collision.gameObject.tag);
	// 	if(collision.gameObject.tag=="") return;
    // 	if (collision.gameObject.tag == "Red"){
    // 		Global.cur_sequence+="R";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    //     else if (collision.gameObject.tag == "Yellow"){
    // 		Global.cur_sequence+="Y";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    //             else if (collision.gameObject.tag == "Sky"){
    // 		Global.cur_sequence+="S";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    //             else if (collision.gameObject.tag == "Pink"){
    // 		Global.cur_sequence+="P";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    //             else if (collision.gameObject.tag == "Blue"){
    // 		Global.cur_sequence+="B";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    //             else if (collision.gameObject.tag == "Green"){
    // 		Global.cur_sequence+="G";
    //         Debug.Log(Global.cur_sequence);
    // 	}
    // }
        private void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
