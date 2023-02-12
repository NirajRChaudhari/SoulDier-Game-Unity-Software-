using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottleController : MonoBehaviour
{

    public TMP_Text globalSequence;
    public TMP_Text nextBottle;
    public TMP_Text messageBox;
    public TMP_Text nextBottleHeader;

    // Start is called before the first frame update
    void Start()
    {
        globalSequence.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);

        globalSequence.text = globalSequence.text.Substring(1);

        if(globalSequence.text != "")
        {
            nextBottle.text = getColorName(globalSequence.text[0]);
        }
        else
        {

            messageBox.text = "LEVEL COMPLETED ";

            nextBottle.text = "";
            nextBottleHeader.text = "";
        }

        messageBox.text="";
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
           
}
