using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;
public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;
    public TMP_Text messageBox;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer spriteRenderer;

    private PlayerController playerController;

    public GameObject checkPoint1, checkPoint2;

    public TMP_Text globalSequence;


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

        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage(string trap)
    {
        if (invincibleCounter <= 0 && gameObject.activeSelf)
        {

            currentHealth--;
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();

            if (trap == "Spike")
            {

                ob.Send("Spike");
            }
            else if (trap == "Rotating Saw")
            {
                ob.Send("Rotating Saw");
            }
            if (currentHealth <= 0)
            {
                messageBox.text = "GAME OVER";
                SendAnalytics4 ob2 = gameObject.AddComponent<SendAnalytics4>();
                // Task.Delay(1000).ContinueWith(t=> ob2.Send("Killed by Traps",PlayerController.level_name));
                ob2.Send("Killed by Traps", PlayerController.level_name);
                Debug.Log("Restarting");
                Debug.Log(playerController.lastCheckpoint);

                if (playerController.lastCheckpoint == "Checkpoint1")
                {
                    Debug.Log(playerController.lastCheckpoint);
                    PlayerPrefs.SetFloat("x", checkPoint1.transform.position.x);
                    PlayerPrefs.SetFloat("y", checkPoint1.transform.position.y);
                    PlayerPrefs.SetString("globalSequenceFile", globalSequence.text);
                    PlayerPrefs.SetString("lastCheckpoint", playerController.lastCheckpoint);
                }
                else if (playerController.lastCheckpoint == "Checkpoint2")
                {
                    Debug.Log(playerController.lastCheckpoint);
                    Debug.Log("CP2 x = " + checkPoint2.transform.position.x);
                    Debug.Log("CP2 y = " + checkPoint2.transform.position.y);
                    PlayerPrefs.SetFloat("x", checkPoint2.transform.position.x);
                    PlayerPrefs.SetFloat("y", checkPoint2.transform.position.y);
                    PlayerPrefs.SetString("globalSequenceFile", globalSequence.text);
                    PlayerPrefs.SetString("lastCheckpoint", playerController.lastCheckpoint);
                }
                SendAnalytics5 ob3 = gameObject.AddComponent<SendAnalytics5>();
                ob3.Send(PlayerController.level_name);
                // Thread.Sleep(5000);
                // gameObject.SetActive(false);
                currentHealth = 0;
                Invoke(nameof(restartLevel), 3f);
            }
            else
            {

                float scaleX = gameObject.transform.localScale.x;
                float scaleY = gameObject.transform.localScale.y;

                gameObject.transform.localScale = new Vector2(scaleX * 0.8f, scaleY * 0.8f);
                invincibleCounter = invincibleLength;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    private void restartLevel()

    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
