using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHeadKillControllerTutorial : MonoBehaviour
{

    private SpriteRenderer enemySpriteRenderer;
    // public TMP_Text messageBox;

    // Start is called before the first frame update
    void Start()
    {
        enemySpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Entered");


        if (collision.gameObject.CompareTag("Player"))
        {

            if (enemySpriteRenderer.color.CompareRGB(collision.gameObject.GetComponent<SpriteRenderer>().color))
            {

                PlayerControllerTutorial pc=collision.gameObject.GetComponent<PlayerControllerTutorial>();
                pc.resetMonsterKillMessageBox();
                // Invoke("PlayerController.resetMonsterKillMessageBox", 1.0f);
                Destroy(transform.parent.gameObject);

                // messageBox.text="";

            }
        }
    }

    // void ResetMessageBox1()
    // {
    //    Debug.Log("in reset box");
    //    messageBox.text = "dafsdf";
    //    Debug.Log(messageBox.text);
    // }
}
