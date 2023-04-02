using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class PlayerControllerTutorial : MonoBehaviour
{
    // Public variables
    public float moveSpeed;
    public float jumpForce;
    public LayerMask whatIsGround;
    public GameObject canvas;
    // public GameObject knobGroup;
    public GameObject checkPointGroup;
    public GameObject blackFloor;
    public GameObject plaformJumpGuideImg;
    public static float totalTime = 1200;
    public static int currentPos = 0;
    public static int currentPosInColorSubseq = 0;
    public static char lastCharInColorSubseq;
    public static int jump_counter;
    public static bool seq_jump_flag;



    // Private variables
    private Rigidbody2D playerRigidbody2D;
    private Transform groundCheckPoint;
    static private char lastChar;
    private bool isGrounded;
    private bool isDoubleJumpAllowed;
    private Animator animator;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerNextColorIndicatorSpriteRenderer;
    // private TMP_Text currentSeq;
    // private TMP_Text currentSeqHeader;
    private TMP_Text targetSeq, targetSeqHeader, messageBox, nextBottle, globalSequence, timerText;
    // private Image knob1, knob2, knob3;
    private GameObject checkPoint1;
    public GameObject checkPoint2;
    private float saveInitialMoveSpeed;
    private float saveInitialJumpForce;

    public GameObject checkpoint3;


    // Start is called before the first frame update
    void Start()
    {
        totalTime = 1200;
        currentPos = 0;
        currentPosInColorSubseq = 0;
        lastCharInColorSubseq = 'A';
        retrieveAndInitializeAllPrivateObjects();

        this.saveInitialMoveSpeed = this.moveSpeed;
        this.saveInitialJumpForce = this.jumpForce;
        blackFloor.SetActive(false);
        playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(false);
        targetSeqHeader.gameObject.SetActive(false);
        targetSeq.gameObject.SetActive(false);

        animator = GetComponent<Animator>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        playerNextColorIndicatorSpriteRenderer.color = getColorUsingCharacter(targetSeq.text[0]);
        messageBox.text = "";
        jump_counter = 0;
        seq_jump_flag = false;
        Invoke(nameof(ResetMessageBox), 10f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isGrounded)
        {
            isDoubleJumpAllowed = true;
        }




        
        playerRigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), playerRigidbody2D.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);



//  isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Mathf.Approximately(playerRigidbody2D.velocity.y, 0))
        {
            if( Mathf.Approximately(Mathf.Abs(playerRigidbody2D.velocity.x), 0)) {
                animator.SetBool("isGrounded", true);
            }
        } else {
            animator.SetBool("isGrounded", false);
        }



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

        if (playerRigidbody2D.velocity.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (playerRigidbody2D.velocity.x > 0) //This condition is important or else at velocity = 0 it will flip X
        {
            playerSpriteRenderer.flipX = false;
        }
        animator.SetFloat("moveSpeed", Mathf.Abs(playerRigidbody2D.velocity.x));
        // animator.SetBool("isGrounded", isGrounded);

        float positionX = transform.position.x;

        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
        }
        else
        {
            totalTime = 0;
            messageBox.text = "TIME'S UP, GAME OVER..";
            // call restartLevel here
            playerRigidbody2D.gameObject.SetActive(false);
            Invoke(nameof(restartLevel), 5f);
        }
        DisplayTime(totalTime);
    }

    private void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        totalTime = 120;
        playerRigidbody2D.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;


        // if (tag.Equals("RedFloor") && lastChar != 'R')
        // {
        //     lastChar = 'R';
        //     playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('R');

        // }
        // else if (tag.Equals("YellowFloor") && lastChar != 'Y')
        // {
        //     lastChar = 'Y';
        //     playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('Y');
        // }
        // else if (tag.Equals("OrangeFloor") && lastChar != 'O')
        // {
        //     lastChar = 'O';
        //     playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('O');

        // }
        // else if (tag.Equals("GreenFloor") && lastChar != 'G')
        // {
        //     lastChar = 'G';
        //     playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('G');

        // }
        // else if (tag.Equals("VioletFloor") && lastChar != 'V')
        // {
        //     lastChar = 'V';
        //     playerNextColorIndicatorSpriteRenderer.color = extractNextColorForPlayerSprite('V');

        // }
        if (tag.Equals("EnemyMonster") || tag.Equals("FireBall"))
        {
            Debug.Log("Fireball touched!");
            totalTime = totalTime - 5;
            messageBox.text = " - 5 Seconds! ";
            Invoke(nameof(ResetMessageBox), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "RedBottle")
        {
            other.gameObject.SetActive(false);
            checkPoint2.SetActive(false);
            playerSpriteRenderer.color = new Color(1, 1, 1, 1);
        }
        if (other.gameObject.tag == "BlueBottle")
        {
            playerSpriteRenderer.color = new Color(1, 0, 0, 1);
            plaformJumpGuideImg.SetActive(false);
        }
        if (other.gameObject.tag == "YellowBottle")
        {
            playerSpriteRenderer.color = new Color(0, 0, 1, 1);
            other.gameObject.SetActive(false);
            checkpoint3.SetActive(false);
            messageBox.text = "Purrfect :)";
            Invoke(nameof(ResetMessageBox), 2f);
        }

        if (other.gameObject.tag == "PlatformJumpGuideImg")
        {
            playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(true);
        }

        if (other.gameObject.tag.Equals("SpeedupPowerup"))
        {
            other.gameObject.SetActive(false);
            // run faster for 3 seconds and again back to normal
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            this.moveSpeed += 4;
            this.jumpForce += 3;
            // playerSpriteRenderer.color = new Color(0, 1, 0, 1);
            Debug.Log("Speed up activated");
            Invoke(nameof(resetMovementToNormal), 3f);
            // totalTime = totalTime + 2;
            // messageBox.text = " + 2 Seconds! ";
            Invoke(nameof(ResetMessageBox), 1f);
        }

        if (other.gameObject.tag.Equals("speedSlowPowerDown"))
        {
            other.gameObject.SetActive(false); 
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            this.moveSpeed -= 4;
            this.jumpForce -= 3;
            // playerSpriteRenderer.color = new Color(1, 0, 0, 1);
            Debug.Log("Speed slow activated");
            Invoke(nameof(resetMovementToNormal), 3f);
        }

         if (other.gameObject.tag.Equals("ColorFreezePowerUp"))
        {
            Debug.Log("ColorFreeze");

            other.gameObject.SetActive(false);

            PlatformControllerTutorial.isFrozen = true;
            
            Invoke(nameof(resetFrozenFlag), 8f);

            //Debug.Log(PlatformControllerTutorial.isFrozen);
        }
    }

    public void resetMovementToNormal()
    {
        Debug.Log("Reset Movement");
        this.moveSpeed = this.saveInitialMoveSpeed;
        this.jumpForce = this.saveInitialJumpForce;
        // playerSpriteRenderer.color = new Color(1, 1, 1, 1);
        playerRigidbody2D.gravityScale = 3;
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
        // Image[] knobGroupImg = knobGroup.GetComponentsInChildren<Image>(true);
        // foreach (Image img in knobGroupImg)
        // {
        //     switch (img.name)
        //     {
        //         case "Knob1":
        //             knob1 = img;
        //             break;

        //         case "Knob2":
        //             knob2 = img;
        //             break;

        //         case "Knob3":
        //             knob3 = img;
        //             break;
        //     }
        // }


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

            }
        }

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

    public void resetFrozenFlag()
    {
        PlatformControllerTutorial.isFrozen = false;
        Debug.Log(PlatformControllerTutorial.isFrozen);
    }

}
