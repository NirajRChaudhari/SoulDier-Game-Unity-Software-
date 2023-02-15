using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;
    private bool isDoubleJumpAllowed;

    public Rigidbody2D player;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public TMP_Text currentSeq;
    public TMP_Text currentSeqHeader;
    public TMP_Text targetSeq;
    public TMP_Text targetSeqHeader;
    public TMP_Text messageBox;
    public GameObject blackBox;
    public int jump_counter;
    private bool seq_jump_flag;
    public bool send_time_up_flag;
    public static float totalTime = 120;
    [SerializeField] private TMP_Text timerText;

    private char lastChar;

    // Start is called before the first frame update
    void Start()
    {
        blackBox.SetActive(false);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSeq.text = "";
        messageBox.text = "Jump on the platform when it's color is same as pickup bottle color.      (Press Spacebar Twice for Double Jump)";
        targetSeqHeader.gameObject.SetActive(false);
        targetSeq.gameObject.SetActive(false);
        currentSeqHeader.gameObject.SetActive(false);
        currentSeq.gameObject.SetActive(false);
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

        if (player.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (player.velocity.x > 0) //This condition is important or else at velocity = 0 it will flip X
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("moveSpeed", Mathf.Abs(player.velocity.x));
        animator.SetBool("isGrounded", isGrounded);

        float positionX = transform.position.x;

        if (positionX > -9 && positionX < -8.5)
        {
            messageBox.text = "";

        }
        else if (positionX > 5 && positionX < 6)
        {
            messageBox.text = "Jump on colors as per the Target sequence.";
            targetSeqHeader.gameObject.SetActive(true);
            targetSeq.gameObject.SetActive(true);
            currentSeqHeader.gameObject.SetActive(true);
            currentSeq.gameObject.SetActive(true);
        }
        else if(positionX > 11.8 && positionX < 12.8)
        {
            messageBox.text = "";
        }
        else if(positionX > 28 && positionX <32)
        {
            messageBox.text = "";
            targetSeqHeader.gameObject.SetActive(false);
            targetSeq.gameObject.SetActive(false);
            currentSeqHeader.gameObject.SetActive(false);
            currentSeq.gameObject.SetActive(false);
        }

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
            Invoke(nameof(restartLevel), 3f);
        }
        DisplayTime(totalTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        // Debug.Log("Touched the floor " + tag);


        if (tag.Equals("RedFloor") && lastChar!='R')
        {
            currentSeq.text += "R";
            lastChar = 'R';
            jump_counter+=1;
            Debug.Log("Touched the floor " + tag);

        }
        else if (tag.Equals("YellowFloor") && lastChar != 'Y')
        {
            currentSeq.text += "Y";
            lastChar = 'Y';
            jump_counter+=1;
            Debug.Log("Touched the floor " + tag);
        }
        else if (tag.Equals("OrangeFloor") && lastChar != 'O')
        {
            currentSeq.text += "O";
            lastChar = 'O';
            jump_counter+=1;
            Debug.Log("Touched the floor " + tag);
        }
        else if (tag.Equals("GreenFloor") && lastChar != 'G')
        {
            currentSeq.text += "G";
            lastChar = 'G';
            jump_counter+=1;
            Debug.Log("Touched the floor " + tag);
        }
        else if (tag.Equals("VioletFloor") && lastChar != 'V')
        {
            currentSeq.text += "V";
            lastChar = 'V';
            jump_counter+=1;
            Debug.Log("Touched the floor " + tag);
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

  

        if (currentSeq.text.Length == (targetSeq.text.Length+1))
        {
            currentSeq.text = currentSeq.text.Substring(1);
        }

        if (currentSeq.text.Equals(targetSeq.text) && transform.position.x<30)
        {
            messageBox.text = "Sequence Satisfied.\n\n Pick the Blue bottle.";
            blackBox.SetActive(true);
            // Debug.Log("adfsdf");
            if (seq_jump_flag==false){
            SendAnalytics2 ob = gameObject.AddComponent<SendAnalytics2>();
            Debug.Log("Jump Counter: "+jump_counter);
            // Debug.Log("seqlen: "+seq_len);
            ob.Send(5, jump_counter);
            seq_jump_flag=true;
            }
            GameObject.Find("RedFloor").GetComponent<SpriteRenderer>().color= new Color(0,0,0,1);
            GameObject.Find("BlueFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            GameObject.Find("OrangeFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            GameObject.Find("GreenFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            GameObject.Find("VioletFloor").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

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

    private void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}