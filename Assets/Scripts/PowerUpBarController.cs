using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject powerUpBar1;
    private GameObject powerUpBar2;
    private float freezeTimer;
    void Start()
    {
        powerUpBar1=GameObject.Find("PowerUpBar1");
        powerUpBar2=GameObject.Find("PowerUpBar2");
        // powerUpBar1.SetActive(false);
        // powerUpBar2.SetActive(false);
        freezeTimer = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformController.isFrozen || PlatformControllerTutorial.isFrozen){
            
            powerUpBar1.SetActive(true);
            powerUpBar2.SetActive(true);
            // powerUpBar1.
            freezeTimer -= Time.deltaTime;
            powerUpBar2.transform.localScale = new Vector3(freezeTimer/8, 1, 1);
        }
        else{
            freezeTimer = 8f;
            powerUpBar1.SetActive(false);
            powerUpBar2.SetActive(false);
        }
    }
    
}
