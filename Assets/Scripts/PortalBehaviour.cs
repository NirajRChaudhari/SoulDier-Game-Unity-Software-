using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Experimental.GraphView.GraphView;

public class PortalBehaviour : MonoBehaviour
{
    private string exit_tag = "";
    private GameObject exit_portal, player;

    private SpriteRenderer portalSpriteRenderer, playerSpriteRenderer;
 private PlayerController playerController;
  private PlayerHealthController playerHealthController;
    private Color[] colors;
    private int colorIndex;

    private float alpha = 1f;
    Color red, blue, yellow;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealthController= GameObject.Find("Player").GetComponent<PlayerHealthController>();
        alpha = 1f;
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
                if (playerController.lastCheckpoint == "Checkpoint1")
                {
                    collision.gameObject.transform.position = new Vector3(playerHealthController.checkPoint1.transform.position.x, playerHealthController.checkPoint1.transform.position.y+20, 0);
            
                    // Debug.Log(playerController.lastCheckpoint);
                    // PlayerPrefs.SetFloat("x", playerHealthController.checkPoint1.transform.position.x);
                    // PlayerPrefs.SetFloat("y", playerHealthController.checkPoint1.transform.position.y);
                    // PlayerPrefs.SetString("globalSequenceFile", globalSequence.text);
                    // PlayerPrefs.SetString("lastCheckpoint", playerController.lastCheckpoint);
                }
                else if (playerController.lastCheckpoint == "Checkpoint2")
                {
                    collision.gameObject.transform.position = new Vector3(playerHealthController.checkPoint2.transform.position.x, playerHealthController.checkPoint2.transform.position.y+20, 0);
                    // PlayerPrefs.SetFloat("x", playerHealthController.checkPoint2.transform.position.x);
                    // PlayerPrefs.SetFloat("y", playerHealthController.checkPoint2.transform.position.y);
                    // PlayerPrefs.SetString("globalSequenceFile", globalSequence.text);
                    // PlayerPrefs.SetString("lastCheckpoint", playerController.lastCheckpoint);
                }
                // playerHealthController.restartLevel();
                // else if (playerController.lastCheckpoint == "StartingPoint")
                else
                {
                    collision.gameObject.transform.position = new Vector3(playerController.initial_x, playerController.initial_y+20, 0);
                    
                }
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

