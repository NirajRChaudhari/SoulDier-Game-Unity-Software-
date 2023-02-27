using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;
    private bool isDoubleJumpAllowed;

    public Rigidbody2D player;
    public Transform groundCheckPoint;
    public GameObject playerNextColorIndicator;
    public LayerMask whatIsGround;
    private Animator animator;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerNextColorIndicatorSpriteRenderer;

    // public TMP_Text currentSeq;
    // public TMP_Text currentSeqHeader;
    public TMP_Text targetSeq;
    public TMP_Text targetSeqHeader;
    public TMP_Text messageBox;

    public TMP_Text globalSequence;

    public Image knob1, knob2, knob3;

    public GameObject blackFloor;

    public static float totalTime = 120;
    public static int currentPos = -1;
    public string lastCheckpoint = "Starting Point";
    public int jump_counter;
    private bool seq_jump_flag;
    public bool send_time_up_flag;

    public GameObject checkPoint1, checkPoint2;

    [SerializeField] private TMP_Text timerText;
    static private char lastChar;

    private float saveInitialMoveSpeed;
    private float saveInitialJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Debug.Log("Latest: " + lastCheckpoint);
            transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        }
        PlayerPrefs.DeleteAll();

        this.saveInitialMoveSpeed = this.moveSpeed;
        this.saveInitialJumpForce = this.jumpForce;
        blackFloor.SetActive(false);
        animator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerNextColorIndicatorSpriteRenderer = playerNextColorIndicator.GetComponent<SpriteRenderer>();

        // currentSeq.text = "";
        messageBox.text = "Jump on the platform when it's color is same as pickup bottle color.";
        Invoke(nameof(ResetMessageBox), 5f);
        targetSeqHeader.gameObject.SetActive(false);
        targetSeq.gameObject.SetActive(false);
        // currentSeqHeader.gameObject.SetActive(false);
        // currentSeq.gameObject.SetActive(false);
        playerNextColorIndicator.SetActive(false);
        jump_counter=0;
        seq_jump_flag=false;
        send_time_up_flag=false;

    }

    public void resetMovementToNormal()
    {
        Debug.Log("Reset Movement");
        this.moveSpeed = this.saveInitialMoveSpeed;
        this.jumpForce = this.saveInitialJumpForce;
        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
        player.gravityScale = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            isDoubleJumpAllowed = true;
        }
        player.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), player.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpForce);
            }
            else
            {
                if (isDoubleJumpAllowed)
                {
                    player.velocity = new Vector2(player.velocity.x, jumpForce);
                    isDoubleJumpAllowed = false;
                }
            }
        }

        if (transform.position.x > checkPoint1.transform.position.x && transform.position.x < checkPoint2.transform.position.x)
        {
            lastCheckpoint = "Checkpoint1";
            Debug.Log(lastCheckpoint);
        }
        else if (transform.position.x > checkPoint2.transform.position.x)
        {
            lastCheckpoint = "Checkpoint2";
            Debug.Log(lastCheckpoint);
        }

        if (player.velocity.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (player.velocity.x > 0) //This condition is important or else at velocity = 0 it will flip X
        {
            playerSpriteRenderer.flipX = false;
        }
        animator.SetFloat("moveSpeed", Mathf.Abs(player.velocity.x));
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
            player.gameObject.SetActive(false);
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
            Debug.Log("speed up activated");
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
            Debug.Log("speed slow activated");
            Invoke(nameof(resetMovementToNormal), 3f);
        }
        if (other.gameObject.tag.Equals("fly"))
        {
            other.gameObject.SetActive(false);
            float normalMoveSpeedSave = this.moveSpeed;
            float normalJumpForce = this.jumpForce;
            player.gravityScale = 1.25f;
            playerSpriteRenderer.color = new Color(1, 1, 0, 1);
            Debug.Log("fly mode activated");
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
            Debug.Log("double jump mode activated");
            messageBox.text = "Double Jump Activated";
            Invoke(nameof(ResetMessageBox), 3f);
            Invoke(nameof(resetMovementToNormal), 5f);
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
        player.gameObject.SetActive(true);
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
                playerNextColorIndicator.SetActive(false);
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
}


