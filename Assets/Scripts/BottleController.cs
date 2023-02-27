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

    public Image knob1, knob2, knob3;

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
            setKnobColor();
        }
        else
        {

            victoryBanner.SetActive(true);
            // run next scene

            player.SetActive(false);

            knob1.gameObject.SetActive(false);
            knob2.gameObject.SetActive(false);
            knob3.gameObject.SetActive(false);

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

            default:
                //nextBottle.color = Color.white;
                return "";
        }

    }

    private void setKnobColor()
    {
        if (nextBottle.text == "Red")
        {
            knob1.color = Color.red;
            knob2.color = new Color(128, 128, 128);
            knob3.color = new Color(128, 128, 128);
        }
        if (nextBottle.text == "Green")
        {
            knob1.color = new Color(128, 128, 128);
            knob2.color = new Color(128, 128, 128);
            knob3.color = Color.green;
        }
        if (nextBottle.text == "Blue")
        {
            knob1.color = new Color(128, 128, 128);
            knob2.color = Color.blue;
            knob3.color = new Color(128, 128, 128);
        }
    }

    private void levelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
