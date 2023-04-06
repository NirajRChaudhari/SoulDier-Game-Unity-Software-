using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PortalBehaviour : MonoBehaviour
{
    private string exit_tag = "";
    private GameObject exit_portal, player;

    private SpriteRenderer portalSpriteRenderer, playerSpriteRenderer;

    private Color[] colors;
    private int colorIndex;

    private float alpha = 0.3f;
    Color red, blue, yellow;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.3f;
        red = Color.red;
        red.a = alpha;
        blue = Color.blue;
        blue.a = alpha;
        yellow = Color.yellow;
        yellow.a = alpha;

        colors = new Color[] { red, yellow, blue };
        colorIndex = 0;
        player = GameObject.Find("Player");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        portalSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        InvokeRepeating("ChangeColor", 0.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerSpriteRenderer.color.r == portalSpriteRenderer.color.r
                && playerSpriteRenderer.color.g == portalSpriteRenderer.color.g
                && playerSpriteRenderer.color.b == portalSpriteRenderer.color.b)
            {
                if (gameObject.tag == "portal_1")
                {
                    exit_tag = "portal_2";
                }
                else if (gameObject.tag == "portal_2")
                {
                    exit_tag = "portal_1";
                }
                else if (gameObject.tag == "portal_3")
                {
                    exit_tag = "portal_4";
                }
                else if (gameObject.tag == "portal_4")
                {
                    exit_tag = "portal_3";
                }
                else if (gameObject.tag == "portal_5")
                {
                    exit_tag = "portal_6";
                }
                else if (gameObject.tag == "portal_6")
                {
                    exit_tag = "portal_5";
                }
                exit_portal = GameObject.FindWithTag(exit_tag);
                collision.gameObject.transform.position = new Vector3(exit_portal.transform.position.x + 2, exit_portal.transform.position.y, 0);
            }
            else
            {
                //PlayerHealthController.instance.currentHealth--;
                //UIController.instance.UpdateHealthDisplay();

                //float scaleX = player.transform.localScale.x;
                //float scaleY = player.transform.localScale.y;

                //player.transform.localScale = new Vector2(scaleX * 0.8f, scaleY * 0.8f);
                //invincibleCounter = invincibleLength;
                //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }
        
        }
    }

    void ChangeColor()
    {
        colorIndex = (colorIndex) % colors.Length;
        portalSpriteRenderer.color = colors[colorIndex];
        colorIndex++;

    }
}

