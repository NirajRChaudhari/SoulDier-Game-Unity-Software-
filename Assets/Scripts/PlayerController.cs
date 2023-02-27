using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed;
    public float jumpForce;
    private Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public GameObject canvas;
    public GameObject knobGroup;
    public GameObject checkPointGroup;
    public GameObject blackFloor;
    public static float totalTime = 120;
    public static int currentPos = -1;
    public string lastCheckpoint = "Starting Point";
    public int jump_counter;
    private bool seq_jump_flag;
    public bool send_time_up_flag;


    // Private variables
    private Rigidbody2D playerRigidbody2D;
    static private char lastChar;
    private bool isGrounded;
    private bool isDoubleJumpAllowed;
    private float saveInitialMoveSpeed;
    private float saveInitialJumpForce;
    private Animator animator;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerNextColorIndicatorSpriteRenderer;
    // private TMP_Text currentSeq;
    // private TMP_Text currentSeqHeader;
    private TMP_Text targetSeq, targetSeqHeader, messageBox, nextBottle, globalSequence, timerText;
    private Image knob1, knob2, knob3;
    private GameObject checkPoint1, checkPoint2;



    // Start is called before the first frame update
    void Start()
    {
        retrieveAndInitializeAllPrivateObjects();



        this.saveInitialMoveSpeed = this.moveSpeed;
        this.saveInitialJumpForce = this.jumpForce;
        blackFloor.SetActive(false);
        animator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // currentSeq.text = "";
        messageBox.text = "Jump on the platform when it's color is same as pickup bottle color.";
        Invoke(nameof(ResetMessageBox), 5f);
        targetSeqHeader.gameObject.SetActive(false);
        targetSeq.gameObject.SetActive(false);
        // currentSeqHeader.gameObject.SetActive(false);
        // currentSeq.gameObject.SetActive(false);
        playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(false);
        jump_counter=0;
        seq_jump_flag=false;
        send_time_up_flag=false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            isDoubleJumpAllowed = true;
        }
        playerRigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), playerRigidbody2D.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce);
            }
            else
            {
                if (isDoubleJumpAllowed)
                {
                    playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce);
                    isDoubleJumpAllowed = false;
                }
            }
        }

        if (transform.position.x > checkPoint1.transform.position.x && transform.position.x < checkPoint2.transform.position.x)
        {
            lastCheckpoint = "Checkpoint1";
        }
        else if (transform.position.x > checkPoint2.transform.position.x)
        {
            lastCheckpoint = "Checkpoint2";
        }

        if (playerRigidbody2D.velocity.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (playerRigidbody2D.velocity.x > 0) //This condition is important or else at velocity = 0 it will flip X
        {
            playerSpriteRenderer.flipX = false;
        }
        animator.SetFloat("moveSpeed", Mathf.Abs(playerRigidbody2D.velocity.x));
        animator.SetBool("isGrounded", isGrounded);

        float positionX = transform.position.x;

        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
        }
        else
        {
            totalTime = 0;
            if (send_time_up_flag==false)
            {
                send_time_up_flag=true;
                SendAnalytics4 ob = gameObject.AddComponent<SendAnalytics4>();
                ob.Send("Time up");
            }
            messageBox.text = "TIME'S UP, GAME OVER..";
            // call restartLevel here
            playerRigidbody2D.gameObject.SetActive(false);
            Invoke(nameof(restartLevel), 5f);
        }
        DisplayTime(totalTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("SpeedupPowerup"))
        {
            other.gameObject.SetActive(false);
            // run faster for 3 seconds and again back to normal
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            this.moveSpeed += 4;
            this.jumpForce += 3;
            playerSpriteRenderer.color = new Color(0, 1, 0, 1);
            Debug.Log("Speed up activated");
            Invoke(nameof(resetMovementToNormal), 3f);
            totalTime = totalTime + 5;
            messageBox.text = " + 5 Seconds! ";
            Invoke(nameof(ResetMessageBox), 1f);
        }

        if (other.gameObject.tag.Equals("speedSlowPowerDown"))
        {
            other.gameObject.SetActive(false);
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            this.moveSpeed -= 4;
            this.jumpForce -= 3;
            playerSpriteRenderer.color = new Color(1, 0, 0, 1);
            Debug.Log("Speed slow activated");
            Invoke(nameof(resetMovementToNormal), 3f);
        }
        if (other.gameObject.tag.Equals("fly"))
        {
            other.gameObject.SetActive(false);
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            playerRigidbody2D.gravityScale = 1.25f;
            playerSpriteRenderer.color = new Color(1, 1, 0, 1);
            Debug.Log("Fly mode activated");
            messageBox.text = "Fly Mode Activated";
            Invoke(nameof(ResetMessageBox), 3f);
            Invoke(nameof(resetMovementToNormal), 6f);
        }
        if (other.gameObject.tag.Equals("doublejump"))
        {
            other.gameObject.SetActive(false);
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            this.jumpForce *= 1.5f;
            playerSpriteRenderer.color = new Color(0, 105, 50, 30);
            Debug.Log("Double jump mode activated");
            messageBox.text = "Double Jump Activated";
            Invoke(nameof(ResetMessageBox), 3f);
            Invoke(nameof(resetMovementToNormal), 5f);
        }

        if(other.gameObject.name == "CP1")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            ob.Send(other.gameObject.name, PlayerController.totalTime);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.name == "CP2")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            ob.Send(other.gameObject.name, PlayerController.totalTime);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.name == "CP3")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            ob.Send(other.gameObject.name, PlayerController.totalTime);
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;


        if (tag.Equals("RedFloor") && lastChar != 'R')
        {
            lastChar = 'R';
            jump_counter+=1;
            playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('R');

        }
        else if (tag.Equals("YellowFloor") && lastChar != 'Y')
        {
            lastChar = 'Y';
            jump_counter+=1;
            playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('Y');

        }
        else if (tag.Equals("OrangeFloor") && lastChar != 'O')
        {
            lastChar = 'O';
            jump_counter+=1;
            playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('O');

        }
        else if (tag.Equals("GreenFloor") && lastChar != 'G')
        {
            lastChar = 'G';
            jump_counter+=1;
            playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('G');

        }
        else if (tag.Equals("VioletFloor") && lastChar != 'V')
        {
            lastChar = 'V';
            jump_counter+=1;
            playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('V');

        }
        else if (tag.Equals("EnemyMonster"))
        {
            totalTime = totalTime - 5;
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            ob.Send("Monster");
            messageBox.text = "5 Seconds Lost...";
            Invoke(nameof(ResetMessageBox), 1f);
        }
                else if        (tag.Equals("FireBall"))
        {
            totalTime = totalTime - 5;
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            messageBox.text = "5 Seconds Lost...";
                ob.Send("Dropping Box");
            Invoke(nameof(ResetMessageBox), 1f);
        }
    }

    private void retrieveAndInitializeAllPrivateObjects()
    {

        // Set All Canvas Child Objects
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        TMP_Text[] all_TMP_Texts = canvas.GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text tmpTextObject in all_TMP_Texts)
        {
            switch (tmpTextObject.name)
            {
                case "TargetSeq":
                    targetSeq = tmpTextObject;
                    break;

                case "TargetSeqHeader":
                    targetSeqHeader = tmpTextObject;
                    break;

                case "MessageBox":
                    messageBox = tmpTextObject;
                    break;

                case "GlobalSequence":
                    globalSequence = tmpTextObject;
                    break;

                case "NextBottle":
                    nextBottle = tmpTextObject;
                    break;

                case "TimerText":
                    timerText = tmpTextObject;
                    break;
            }
        }

        // Set All Knob Child Objects
        Image[] knobGroupImg = knobGroup.GetComponentsInChildren<Image>(true);
        foreach (Image img in knobGroupImg)
        {
            switch (img.name)
            {
                case "Knob1":
                    knob1 = img;
                    break;

                case "Knob2":
                    knob2 = img;
                    break;

                case "Knob3":
                    knob3 = img;
                    break;
            }
        }


        // Set Transform Child Object of Player
        Transform[] groundCheckPoint = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in groundCheckPoint)
        {
            if (t.gameObject.name.Equals("GroundPoint"))
            {
                this.groundCheckPoint = t;
            }
        }

        // Set Sprite Renderer Child Object of Player
        SpriteRenderer[] allSpriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in allSpriteRenderers)
        {
            if (spriteRenderer.gameObject.name.Equals("NextColorIndicator"))
            {
                playerNextColorIndicatorSpriteRenderer = spriteRenderer;
            }
        }

        //Set All Checkpoint Child Objects
        Transform[] allCheckpoints = checkPointGroup.GetComponentsInChildren<Transform>();
        foreach (Transform checkpointTransform in allCheckpoints)
        {
            switch (checkpointTransform.gameObject.name)
            {
                case "Checkpoint1":
                    checkPoint1 = checkpointTransform.gameObject;
                    break;

                case "Checkpoint2":
                    checkPoint2 = checkpointTransform.gameObject;
                    break;
            }
        }

        // Set Player Prefs And Next Bottle Color
        setPlayerPrefsAndNextBottleColor();
    }

    private void setPlayerPrefsAndNextBottleColor()
    {
        if (PlayerPrefs.HasKey("globalSequenceFile") && PlayerPrefs.HasKey("lastCheckpoint"))
        {
            globalSequence.text = PlayerPrefs.GetString("globalSequenceFile");
            if (globalSequence.text != "")
            {
                nextBottle.text = getColorOfBottle(globalSequence.text[0]);
                setKnobColor();
            }
        }

        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Debug.Log("Latest: " + lastCheckpoint);
            transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        }
        PlayerPrefs.DeleteAll();
    }

    public void resetMovementToNormal()
    {
        Debug.Log("Reset Movement");
        this.moveSpeed = this.saveInitialMoveSpeed;
        this.jumpForce = this.saveInitialJumpForce;
        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
        playerRigidbody2D.gravityScale = 3;
    }


    void DisplayTime(float time)
    {
        if (time < 0)
            time = 0;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = "Time- " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

    private void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        messageBox.text = "";
        totalTime = 120;
        playerRigidbody2D.gameObject.SetActive(true);
    }

    private Color extractNextColorForPlayerSprite(char currentPlatformColor)
    {

        Color color = Color.white;

        if (currentPos < targetSeq.text.Length && targetSeq.text[currentPos] == currentPlatformColor)
        {
            currentPos = currentPos + 1;
            if (currentPos == targetSeq.text.Length)
            {
                messageBox.text = "Pick the Blue bottle.";
                blackFloor.SetActive(true);

                GameObject.Find("RedFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                GameObject.Find("BlueFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                GameObject.Find("OrangeFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                GameObject.Find("GreenFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                GameObject.Find("VioletFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        if (seq_jump_flag==false){
                    SendAnalytics2 ob = gameObject.AddComponent<SendAnalytics2>();
                    Debug.Log("Jump Counter: "+jump_counter);
                    // Debug.Log("seqlen: "+seq_len);
                    ob.Send(5, jump_counter);
                    seq_jump_flag=true;
                    }
                playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(false);
                Invoke(nameof(ResetMessageBox), 6f);

                return extractNextColorForPlayerSprite(currentPlatformColor);
            }
        }
        else
        {
            currentPos = 0;
        }
        color = getColorUsingCharacter(targetSeq.text[currentPos]);


        return color;
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

    private string getColorOfBottle(char ch)
    {

        switch (ch)
        {
            case 'R':
                return "Red";
            case 'B':
                return "Blue";
            case 'G':
                return "Green";

            default:
                return "Gray";
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
}


