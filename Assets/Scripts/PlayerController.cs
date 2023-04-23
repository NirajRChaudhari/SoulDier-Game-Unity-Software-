using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
// using System.Threading.Tasks;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeedInput;
    public float moveSpeed;
    public float jumpForceInput;
    public float jumpForce;
    private Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public GameObject canvas;
    public GameObject checkPointGroup;
    public GameObject blackFloor;
    public ParticleSystem[] premParticles;
    public static float totalTime = 120;
    public string lastCheckpoint = "Starting Point";
    public static int jump_counter;
    public static bool seq_jump_flag;
    public bool send_time_up_flag;
    private bool TimerColorRed = false;
    private bool lessTimeFlag = false;


    // Private variables
    private Rigidbody2D playerRigidbody2D;
    private char lastChar;

    public static char lastCharInColorSubseq;
    public static int currentPosInColorSubseq = -1;

    private bool isGrounded;
    private bool isDoubleJumpAllowed;
    private float saveInitialMoveSpeed;
    private float saveInitialJumpForce;
    private Animator animator;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerNextColorIndicatorSpriteRenderer;
    private TMP_Text targetSeq, targetSeqHeader, nextBottle, globalSequence, timerText;
    public static TMP_Text messageBox;
    private GameObject checkPoint1, checkPoint2, doubleJumpAnimationObject;
    public static string level_name;
    private float prev_time = 0;
    private float _time_taken;
    private long _sessionId;

    public static bool send_analytics_1_enabled = false;
    public static bool send_analytics_2_enabled = false;
    public static bool send_analytics_3_enabled = false;
    public static bool send_analytics_4_enabled = false;
    public static bool send_analytics_5_enabled = false;
    public static bool send_analytics_6_enabled = false;
    // private bool cooldown = false;
    //  public static bool send_analytics_1_enabled = true;
    // public static bool send_analytics_2_enabled = true;
    // public static bool send_analytics_3_enabled = true;
    // public static bool send_analytics_4_enabled = true;
    // public static bool send_analytics_5_enabled = true;
    // public static bool send_analytics_6_enabled = true;

    // Start is called before the first frame update
    public float initial_x;
    public float initial_y;

    void Start()
    {
        //Invert Controls Flas Initialization
        InvertedSphere.inTheSphereZone = false;
        InvertedSphere.isSphereActive = false;
        InvertedSphere.invertControls = false;

        PlatformControllerTutorial.isFrozen = false;
        PlatformController.isFrozen = false;
        initial_x = transform.position.x;
        initial_y = transform.position.y;
        this.jumpForce = this.jumpForceInput;
        this.moveSpeed = this.moveSpeedInput;
        Debug.Log(this.moveSpeed);
        premParticles = GameObject.FindObjectsOfType<ParticleSystem>();
        // Debug.Log(premParticles);
        foreach (ParticleSystem particle in premParticles)
        {
            particle.Stop();
        }


        currentPosInColorSubseq = -1;
        lastCharInColorSubseq = 'A';
        prev_time = 0;
        lastCheckpoint = "Starting Point";


        GameObject.Find("gameOverScreen").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);

        retrieveAndInitializeAllPrivateObjects();

        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        //Time for level
        if (!RespawnCheck.isRespawn)
        {
            if (scene.name == "level1" || scene.name == "level2" || scene.name == "level3" || scene.name == "level4" || scene.name == "level5" || scene.name == "level6" || scene.name == "level7")
            {
                totalTime = 1200;
            }
            else
            {
                totalTime = 150;
            }
        }

        level_name = scene.name;
        _sessionId = DateTime.Now.Ticks;
        this.saveInitialMoveSpeed = this.moveSpeed;
        this.saveInitialJumpForce = this.jumpForce;
        blackFloor.SetActive(false);
        animator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // messageBox.text = "Jump on the platform when it's color is same as pickup bottle color.";
        // Invoke(nameof(ResetMessageBox), 5f);
        targetSeqHeader.gameObject.SetActive(false);
        targetSeq.gameObject.SetActive(false);
        playerNextColorIndicatorSpriteRenderer.gameObject.SetActive(false);

        jump_counter = 0;
        seq_jump_flag = false;
        send_time_up_flag = false;
        timerText.fontSize = 52;
        timerText.fontStyle = FontStyles.Bold;

        // InvokeRepeating("reverseCooldown",0,0f,1.0f);

        // timerText.fontStyle = FontStyles.UpperCase;

        PreReqText ob = new PreReqText();
        messageBox.text = ob.getText(level_name);
        // messageBox.fontSize = 100;
        Invoke(nameof(ResetMessageBox), 2f);

        Transform[] playerObjectChildrens = transform.gameObject.GetComponentsInChildren<Transform>();
        Debug.Log(playerObjectChildrens.Length);
        foreach (Transform children in playerObjectChildrens)
        {
            if (children.tag.Equals("DoubleJumpAnimation"))
            {
                doubleJumpAnimationObject = children.gameObject;
            }
        }
        doubleJumpAnimationObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        playerSpriteRenderer.color = getColorUsingColorName(nextBottle.text);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);


        //----------------------------Invert Logic--------------------------
        if (InvertedSphere.inTheSphereZone && InvertedSphere.invertControls)
            playerRigidbody2D.velocity = new Vector2(-moveSpeed * Input.GetAxis("Horizontal"), playerRigidbody2D.velocity.y);
        else
            playerRigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), playerRigidbody2D.velocity.y);

        //if(isGrounded) {
        //    isDoubleJumpAllowed = true;
        //    this.animator.SetBool("isGrounded", true);
        //    //this.animator.SetBool("doubleJumpAllowed",true);

        //}

        if (Input.GetButtonDown("Jump"))
        {
            if (isDoubleJumpAllowed || isGrounded)
            {
                doubleJumpAnimationObject.SetActive(true);
                Invoke("disableDoubleJumpAnimation", 0.1f);

                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce);

                if (isDoubleJumpAllowed)
                {
                    //this.animator.SetBool("doubleJumpAllowed", false);
                    //animator.SetBool("isGrounded", false);
                    isDoubleJumpAllowed = false;
                }
                else
                {
                    //animator.SetBool("doubleJumpAllowed", true);
                    isDoubleJumpAllowed = true;
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
        //Debug.Log("isGrounded == >"+ isGrounded);
        animator.SetBool("isGrounded", isGrounded);

        float positionX = transform.position.x;

        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
        }
        else
        {
            totalTime = 0;
            if (send_time_up_flag == false)
            {
                send_time_up_flag = true;
                SendAnalytics4 ob = gameObject.AddComponent<SendAnalytics4>();
                // Task.Delay(1000).ContinueWith(t=> ob.Send("Time up",level_name));
                ob.Send("Time up", level_name);
                SendAnalytics5 ob3 = gameObject.AddComponent<SendAnalytics5>();
                ob3.Send(PlayerController.level_name);
            }
            messageBox.text = "TIME'S UP, GAME OVER..";
            messageBox.fontSize = 100;
            messageBox.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 500);
            messageBox.color = new Color(255f, 255f, 255f, 1.0f);
            GameObject.Find("gameOverScreen").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.8f);
            this.moveSpeed = 0f;
            this.jumpForce = 0f;
            // call restartLevel here

            // Thread.sleep(2000);
            // Thread.Sleep(1000);
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
            // totalTime = totalTime + 5;
            // messageBox.text = " + 5 Seconds! ";
            Invoke(nameof(ResetMessageBox), 1f);
            SendAnalytics6 ob = gameObject.AddComponent<SendAnalytics6>();
            // Task.Delay(1000).ContinueWith(t=> ob.Send("Time up",level_name));
            ob.Send("Speed up");
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
            SendAnalytics6 ob = gameObject.AddComponent<SendAnalytics6>();
            // Task.Delay(1000).ContinueWith(t=> ob.Send("Time up",level_name));
            ob.Send("Slow down");
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
            SendAnalytics6 ob = gameObject.AddComponent<SendAnalytics6>();
            // Task.Delay(1000).ContinueWith(t=> ob.Send("Time up",level_name));
            ob.Send("Fly");
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

        if (other.gameObject.tag.Equals("HeartPowerUp"))
        {
            Debug.Log("HeartPowerUp");
            Debug.Log(PlayerHealthController.instance.currentHealth);


            if (PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                other.gameObject.SetActive(false);

                PlayerHealthController.instance.currentHealth++;
                UIController.instance.UpdateHealthDisplay();

                float scaleX = gameObject.transform.localScale.x;
                float scaleY = gameObject.transform.localScale.y;

                gameObject.transform.localScale = new Vector2(scaleX * 1.25f, scaleY * 1.25f);
                //invincibleCounter = invincibleLength;
                //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }
            else
            {
                messageBox.text = "Health is full";
                Invoke(nameof(ResetMessageBox), 2f);

            }

            Debug.Log(PlayerHealthController.instance.currentHealth);
        }

        if (other.gameObject.tag.Equals("TimeIncreasePowerUp"))
        {
            other.gameObject.SetActive(false);
            messageBox.text = "+5 seconds!";
            Invoke(nameof(ResetMessageBox), 2f);
            PlayerController.totalTime += 5f;
        }

        if (other.gameObject.tag.Equals("ColorFreezePowerUp"))
        {
            Debug.Log("ColorFreeze");

            other.gameObject.SetActive(false);

            PlatformController.isFrozen = true;

            Invoke(nameof(resetFrozenFlag), 8f);

            //Debug.Log(PlatformController.isFrozen);
        }

        if (other.gameObject.name == "CP1")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            _time_taken = 120f - PlayerController.totalTime;
            ob.Send(other.gameObject.name, _time_taken, level_name, _sessionId);

            other.gameObject.SetActive(false);
            prev_time = _time_taken;
        }

        if (other.gameObject.name == "CP2")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            _time_taken = 120f - PlayerController.totalTime;

            ob.Send(other.gameObject.name, _time_taken - prev_time, level_name, _sessionId);
            other.gameObject.SetActive(false);
            prev_time = _time_taken;
        }

        if (other.gameObject.name == "CP3")
        {
            SendAnalytics ob = gameObject.AddComponent<SendAnalytics>();
            _time_taken = 120f - PlayerController.totalTime;
            ob.Send(other.gameObject.name, _time_taken - prev_time, level_name, _sessionId);
            other.gameObject.SetActive(false);
            SendAnalytics4 ob2 = gameObject.AddComponent<SendAnalytics4>();
            ob2.Send("Level Completed", level_name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("EnemyMonster") && !collision.gameObject.GetComponent<SpriteRenderer>().color.CompareRGB(gameObject.GetComponent<SpriteRenderer>().color))
        {
            totalTime = totalTime - 5;
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            ob.Send("Monster");
            messageBox.text = "5 Seconds Lost...";
            Invoke(nameof(ResetMessageBox), 1f);
        }
        else if (tag.Equals("FireBall"))
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
        if (PlayerPrefs.HasKey("lastCheckpoint"))
        {
            lastCheckpoint = PlayerPrefs.GetString("lastCheckpoint");
        }

        if (PlayerPrefs.HasKey("globalSequenceFile") && PlayerPrefs.HasKey("lastCheckpoint"))
        {
            globalSequence.text = PlayerPrefs.GetString("globalSequenceFile");

            if (globalSequence.text != "")
            {
                nextBottle.text = getColorOfBottle(globalSequence.text[0]);
            }
        }
        if (PlayerPrefs.HasKey("lastCheckpoint"))
        {
            if (PlayerPrefs.GetString("lastCheckpoint").Equals("Checkpoint1"))
            {
                GameObject.Find("RedBottle").SetActive(false);
            }
            else if (PlayerPrefs.GetString("lastCheckpoint").Equals("Checkpoint2"))
            {
                GameObject.Find("RedBottle").SetActive(false);
                GameObject.Find("BlueBottle").SetActive(false);
            }
        }

        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Debug.Log("Latest: " + lastCheckpoint);
            Debug.Log("X: " + PlayerPrefs.GetFloat("x"));
            Debug.Log("Y: " + PlayerPrefs.GetFloat("y"));
            transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));

            playerNextColorIndicatorSpriteRenderer.color = SequencePlatformController.getColorUsingCharacter(targetSeq.text[0]);
            currentPosInColorSubseq = 0;

            Scene currentScene = SceneManager.GetActiveScene();

            //Logic specific to Level 2 
            if (currentScene.name == "FinalLvl2")
            {
                if (lastCheckpoint == "Checkpoint1")
                {
                    currentPosInColorSubseq = 0;
                    lastCharInColorSubseq = 'A';
                }
            }
            else if (currentScene.name == "FinalLvl3")
            {
                Debug.Log("Print checkpoint " + lastCheckpoint);
                if (lastCheckpoint == "Checkpoint1")
                {
                    currentPosInColorSubseq = 0;
                    lastCharInColorSubseq = 'A';
                }
            }
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
        timerText.text = "TIME - " + string.Format("{0:00}:{1:00}", minutes, seconds);
        // Debug.Log(time);
        if (time < 20 && lessTimeFlag == false)
        {
            Debug.Log("Time is 145");
            // InvokeRepeating("changeTimerColor", 1.0f);
            InvokeRepeating("changeTimerColor", 0.0f, 1.0f);
            lessTimeFlag = true;
        }


    }
    void changeTimerColor()

    {
        if (TimerColorRed == true)
        {
            timerText.color = Color.red;
            TimerColorRed = false;
        }
        else
        {
            timerText.color = Color.white;
            TimerColorRed = true;
        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }

    private void restartLevel()
    {
        RespawnCheck.isRespawn = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        messageBox.text = "";
        //totalTime = 120;
        playerRigidbody2D.gameObject.SetActive(true);
        playerNextColorIndicatorSpriteRenderer.color = SequencePlatformController.getColorUsingCharacter(targetSeq.text[0]);
        currentPosInColorSubseq = 0;
        lastCharInColorSubseq = 'A';
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
            case 'Y':
                return "Yellow";
            default:
                return "White";
        }
    }

    static public Color getColorUsingColorName(string colorName)
    {

        switch (colorName)
        {
            case "Red":
                return Color.red;

            case "Blue":
                return Color.blue;

            case "Green":
                return Color.green;

            case "Yellow":
                return Color.yellow;

            default:
                return Color.black;
        }
    }

    public void resetFrozenFlag()
    {
        PlatformController.isFrozen = false;
        Debug.Log(PlatformController.isFrozen);
    }

    public void resetMonsterKillMessageBox()
    {
        PlayerController.totalTime += 15f;
        messageBox.text = "15 Seconds Gained...";
        Invoke("resetMessageBox2", 1.0f);
        // PlatformController.isFrozen = false;
        // Debug.Log(PlatformController.isFrozen);
    }

    public void resetMessageBox2()
    {
        messageBox.text = "";
        // PlatformController.isFrozen = false;
        // Debug.Log(PlatformController.isFrozen);
    }

    private void disableDoubleJumpAnimation()
    {
        doubleJumpAnimationObject.SetActive(false);
    }
}


