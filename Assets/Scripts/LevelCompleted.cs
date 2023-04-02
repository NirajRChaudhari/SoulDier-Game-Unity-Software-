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
            messageBox.text = "Congrats! Level Completed";
            Invoke("levelComplete", 3f);
            messageBox.text = "Congrats! Level Completed";
        }
    }

    private void levelComplete() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("ChangeLvl");
    }
    
}
