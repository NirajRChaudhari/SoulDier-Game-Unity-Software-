using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequencePlatformController : MonoBehaviour
{
    public GameObject playerNextColorIndicator;
    public GameObject canvas;
    public GameObject colorfullFloor;
    public GameObject blackFloor;

    private SpriteRenderer playerNextColorIndicatorSpriteRenderer, currentPlatformSpriteRenderer;
    private TMP_Text targetSeq, messageBox;
    private SpriteRenderer redFloor, greenFloor, orangeFloor, yellowFloor, violetFloor;

    // Start is called before the first frame update
    void Start()
    {
        currentPlatformSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerNextColorIndicatorSpriteRenderer = playerNextColorIndicator.GetComponent<SpriteRenderer>();

        retrieveAndInitializeAllPrivateObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void retrieveAndInitializeAllPrivateObjects()
    {

        TMP_Text[] all_TMP_Texts = canvas.GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text tmpTextObject in all_TMP_Texts)
        {
            switch (tmpTextObject.name)
            {
                case "TargetSeq":
                    targetSeq = tmpTextObject;
                    break;

                case "MessageBox":
                    messageBox = tmpTextObject;
                    break;
            }
        }


        SpriteRenderer[] allColorfullFloors = colorfullFloor.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer colorfullFloor in allColorfullFloors)
        {
            switch (colorfullFloor.name)
            {
                case "RedFloor":
                    redFloor = colorfullFloor;
                    break;

                case "GreenFloor":
                    greenFloor = colorfullFloor;
                    break;

                case "OrangeFloor":
                    orangeFloor = colorfullFloor;
                    break;

                case "YellowFloor":
                    yellowFloor = colorfullFloor;
                    break;

                case "VioletFloor":
                    violetFloor = colorfullFloor;
                    break;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !currentPlatformSpriteRenderer.color.CompareRGB(Color.white))
        {
            if (PlayerController.lastCharInColorSubseq != gameObject.name[0] || PlayerController.currentPosInColorSubseq == 0)
            {
                PlayerController.jump_counter = PlayerController.jump_counter + 1;
                PlayerController.lastCharInColorSubseq = gameObject.name[0];
                playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite(gameObject.name[0]);
            }
        }
    }

    private Color extractNextColorForPlayerSprite(char currentPlatformColor)
    {

        Color color = Color.white;
        Debug.Log("Point 1");

        if (PlayerController.currentPosInColorSubseq < targetSeq.text.Length && targetSeq.text[PlayerController.currentPosInColorSubseq] == currentPlatformColor)
        {
            PlayerController.currentPosInColorSubseq = PlayerController.currentPosInColorSubseq + 1;
            Debug.Log("Point 2");
            Color platformColor = currentPlatformSpriteRenderer.color;
            platformColor.a = 0.5f;
            currentPlatformSpriteRenderer.color = platformColor;

            Debug.Log("Point 3");
            if (PlayerController.currentPosInColorSubseq == targetSeq.text.Length)
            {
                Debug.Log("Point 4");

                messageBox.text = "Pick the Blue bottle.";
                blackFloor.SetActive(true);

                redFloor.color = Color.white;
                yellowFloor.color = Color.white;
                orangeFloor.color = Color.white;
                greenFloor.color = Color.white;
                violetFloor.color = Color.white;
                Debug.Log("Point 7");

                if (PlayerController.seq_jump_flag == false)
                {
                    Debug.Log("Point 5");

                    SendAnalytics2 ob = gameObject.AddComponent<SendAnalytics2>();
                    Debug.Log("Jump Counter: " + PlayerController.jump_counter);
                    // Debug.Log("seqlen: "+seq_len);
                    ob.Send(5, PlayerController.jump_counter, PlayerController.level_name);
                    PlayerController.seq_jump_flag = true;
                }
                Debug.Log("Point 6");

                playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(false);
                Invoke(nameof(ResetMessageBox), 6f);

                return Color.white;
            }
        }
        else
        {
            Debug.Log("Point 8");

            PlayerController.currentPosInColorSubseq = 0;

            redFloor.color = Color.cyan;
            yellowFloor.color = Color.grey;
            orangeFloor.color = new Color(1, 0.5f, 0, 1);
            greenFloor.color = Color.green;
            violetFloor.color = Color.magenta;
        }
        Debug.Log("Starting method");
        color = getColorUsingCharacter(targetSeq.text[PlayerController.currentPosInColorSubseq]);
        Debug.Log("Ending method");

        return color;
    }

    public static Color getColorUsingCharacter(char colorChar)
    {
        Color color = Color.white;

        switch (colorChar)
        {
            case 'R':
                color = Color.cyan;
                break;
            case 'Y':
                color = Color.grey;
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

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

}
