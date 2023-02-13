using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControllerTutorial : MonoBehaviour
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

    private float totalTime = 90;
    [SerializeField] private TMP_Text timerText;

    private char lastChar;

    // Start is called before the first frame update
    void Start()
    {
        blackBox.SetActive(false);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSeq.text = "";
        messageBox.text = "Match current and target sequence! Jump and Try :)";
        targetSeqHeader.gameObject.SetActive(true);
        targetSeq.gameObject.SetActive(true);
        currentSeqHeader.gameObject.SetActive(true);
        currentSeq.gameObject.SetActive(true);
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
        else if (positionX > -1 && positionX < 3)
        {
            messageBox.text = "Match current and target sequence! Jump and Try :)";
            targetSeqHeader.gameObject.SetActive(true);
            targetSeq.gameObject.SetActive(true);
            currentSeqHeader.gameObject.SetActive(true);
            currentSeq.gameObject.SetActive(true);
        }
        else if(positionX > 11.8 && positionX < 12.8)
        {
            messageBox.text = "";
        }
        else if(positionX > 28 && positionX <29)
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
            messageBox.text = "TIME'S UP, GAME OVER..";
            player.gameObject.SetActive(false);
        }
        DisplayTime(totalTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        Debug.Log("Touched the floor " + tag);

        if (tag.Equals("RedFloor") && lastChar!='R')
        {
            currentSeq.text += "R";
            lastChar = 'R';
        }
        else if (tag.Equals("YellowFloor") && lastChar != 'Y')
        {
            currentSeq.text += "Y";
            lastChar = 'Y';
        }
        else if (tag.Equals("OrangeFloor") && lastChar != 'O')
        {
            currentSeq.text += "O";
            lastChar = 'O';
        }
        else if (tag.Equals("GreenFloor") && lastChar != 'G')
        {
            currentSeq.text += "G";
            lastChar = 'G';
        }
        else if (tag.Equals("VioletFloor") && lastChar != 'V')
        {
            currentSeq.text += "V";
            lastChar = 'V';
        }
        else if (tag.Equals("EnemyMonster") || tag.Equals("FireBall"))
        {
            totalTime = totalTime - 5;
            messageBox.text = "5 Seconds Lost...";
            Invoke(nameof(ResetMessageBox), 1f);
        }
  

        if (currentSeq.text.Length == (targetSeq.text.Length+1))
        {
            currentSeq.text = currentSeq.text.Substring(1);
        }

        if (currentSeq.text.Equals(targetSeq.text))
        {
            // messageBox.text = "Sequence Satisfied.\n\n Pick the Blue bottle.";
            blackBox.SetActive(true);

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
}