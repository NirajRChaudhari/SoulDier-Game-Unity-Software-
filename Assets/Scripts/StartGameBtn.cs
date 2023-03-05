using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour
{
    // SceneManager.GetActiveScene().buildIndex + 1

    public void tutorialStart() {
        PlayerControllerTutorial.totalTime = 1200;
        SceneManager.LoadScene("tutorial");
    }

    public void Level1Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("FinalLvl1");
    }

    public void Level2Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("FinalLvl2");
    }

    public void Level3Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("FinalLvl3");
    }
}
