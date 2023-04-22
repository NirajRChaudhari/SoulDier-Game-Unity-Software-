using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottleController : MonoBehaviour
{

    public TMP_Text globalSequence;
    public TMP_Text nextBottle;
    public TMP_Text messageBox;
    public TMP_Text nextBottleHeader;
    public GameObject victoryBanner;
    public GameObject player;
    public GameObject dependentCheckpoint;


    // Start is called before the first frame update
    void Start()
    {
        globalSequence.gameObject.SetActive(false);
        victoryBanner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        dependentCheckpoint.GetComponent<BoxCollider2D>().isTrigger = true;

        globalSequence.text = globalSequence.text.Substring(1);

        if (globalSequence.text != "")
        {
            nextBottle.text = getColorName(globalSequence.text[0]);
        }
        else
        {

            victoryBanner.SetActive(true);
            // run next scene

            player.SetActive(false);

            nextBottle.text = "";
            nextBottleHeader.text = "";
            Invoke("levelComplete", 3f);
        }
        messageBox.text = "";
    }

    private string getColorName(char colorCode)
    {
        switch (colorCode)
        {
            case 'R':
                //nextBottle.color = Color.red;
                return "Red";

            case 'B':
                //nextBottle.color = Color.blue;

                return "Blue";

            case 'G':
                //nextBottle.color = Color.green;
                return "Green";

            case 'Y':
                return "Yellow";

            default:
                //nextBottle.color = Color.white;
                return "";
        }

    }

    private void levelComplete()
    {
        PlayerController.totalTime = 120;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("LevelSelectorScreen");
    }
}
