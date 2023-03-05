using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour
{
    // SceneManager.GetActiveScene().buildIndex + 1

    public void tutorialStart() {
        SceneManager.LoadScene(1);
    }

    public void Level1Start() {
        SceneManager.LoadScene(3);
    }

    public void Level2Start() {
        SceneManager.LoadScene(5);
    }

    public void Level3Start() {
        SceneManager.LoadScene(7);
    }
}
