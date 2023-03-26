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

    public void Level0Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("FinalLvl0");
    }

    public void Level1Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("FinalLvl1");
    }

    public void Level2Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("Prem Level");
    }

    public void Level3Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("PremLevel2");
    }

    public void Level4Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("Parth01");
    }

    public void Level5Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("Parth02");
    }

    public void Level6Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("PremLevel3");
    }

    public void Level7Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("Parth03");
    }
}
