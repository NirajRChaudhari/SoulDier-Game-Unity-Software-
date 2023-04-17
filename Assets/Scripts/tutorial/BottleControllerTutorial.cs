using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottleControllerTutorial : MonoBehaviour
{

    public TMP_Text globalSequence;
    public TMP_Text messageBox;
    public GameObject player;
    public GameObject dependentCheckpoint;

    private SpriteRenderer playerSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        globalSequence.gameObject.SetActive(false);
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        // victoryBanner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerSpriteRenderer.color = new Color(1, 0, 0, 1);
        gameObject.SetActive(false);
        dependentCheckpoint.GetComponent<BoxCollider2D>().isTrigger = true;

        globalSequence.text = globalSequence.text.Substring(1);

        if (globalSequence.text != "")
        {
            // nextBottle.text = getColorName(globalSequence.text[0]);
        }
        else
        {

            // victoryBanner.SetActive(true);
            player.SetActive(false);

            // nextBottle.text = "";
            // nextBottleHeader.text = "";
        }

        messageBox.text = "";
    }

    private string getColorName(char colorCode)
    {
        switch (colorCode)
        {

            case 'B':
                //nextBottle.color = Color.blue
                return "Blue";

            default:
                //nextBottle.color = Color.white;
                return "";
        }

    }

}
