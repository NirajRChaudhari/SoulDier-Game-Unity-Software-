using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonOnCanvas : MonoBehaviour
{
    public void mainMenuScreen()
    {
        // PlatformController.isFrozen=false;
        SceneManager.LoadScene("LevelSelectorScreen");
    }
}
