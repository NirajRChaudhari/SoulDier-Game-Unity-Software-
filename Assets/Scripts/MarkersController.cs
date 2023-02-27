using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkersController : MonoBehaviour
{

    public TMP_Text messageBox;

    public TMP_Text targetSeq;

    public GameObject playerNextColorIndicator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name.Equals("Markers1"))
            {
                gameObject.SetActive(false);

                messageBox.text = "Jump on platform with the color same as Marker.";
                playerNextColorIndicator.SetActive(true);

                if (PlayerController.currentPos == -1)
                {
                    PlayerController.currentPos = 0;
                    playerNextColorIndicator.GetComponent<SpriteRenderer>().color = getColorUsingCharacter(targetSeq.text[0]);
                }
                Invoke(nameof(ResetMessageBox), 8f);
            }

        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

    private Color getColorUsingCharacter(char colorChar)
    {
        Color color = Color.white;

        switch (colorChar)
        {
            case 'R':
                color = Color.red;
                break;
            case 'Y':
                color = Color.yellow;
                break;
            case 'O':
                color = new Color(1, 0.5f, 0, 1);
                break;
            case 'G':
                color = Color.green;
                break;
            case 'V':
                color = Color.magenta;
                break;
        }

        return color;
    }
}
