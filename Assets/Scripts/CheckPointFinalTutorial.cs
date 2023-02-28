using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointFinalTutorial : MonoBehaviour
{
    public TMP_Text messageBox;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            messageBox.text = "Pick up the Bottle to go ahead. ";
            Invoke(nameof(ResetMessageBox), 4f);
        }
    }

    void ResetMessageBox()
    {
        messageBox.text = "";
    }
}
