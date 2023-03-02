using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleted : MonoBehaviour
{
    
    public TMP_Text messageBox;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name=="Player") {
            messageBox.text = "Congrats! Starting game...";
            Invoke("levelComplete", 3f);
            messageBox.text = "Congrats! Starting game...";
        }
    }

    private void levelComplete() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
