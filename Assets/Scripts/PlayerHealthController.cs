using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}
