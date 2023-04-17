using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour
{
    // SceneManager.GetActiveScene().buildIndex + 1

    public void tutorialStart() {
        PlayerControllerTutorial.totalTime = 1200;
        SceneManager.LoadScene("tutorial_Combined");
    }

    public void Level1Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("level1");
    }

    public void Level2Start() {
        PlayerController.totalTime = 120;
        SceneManager.LoadScene("level2");
    }

    public void Level3Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level3");
    }

    public void Level4Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level4");
    }

    public void Level5Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level5");
    }

    public void Level6Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level6");
    }

    public void Level7Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level7");
    }

    public void Level8Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level8");
    }

    public void Level9Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level9");
    }
    public void Level10Start() {
        PlayerController.totalTime = 150;
        SceneManager.LoadScene("level10");
    }

    public void Tutorial2Start() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("Tutorial_6");
    }

    public void TutorialLevel1() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_1");
    }

    public void TutorialLevel2() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_2");
    }

    public void TutorialLevel3() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_3");
    }

    public void TutorialLevel4() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_4");
    }

    public void TutorialLevel5() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_5");
    }

    public void TutorialLevel7() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_7");
    }
    
    public void TutorialLevel8() {
        PlayerController.totalTime = 1200;
        SceneManager.LoadScene("tutorial_8");
    }
}
