using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator anim;
    [SerializeField] private AudioSource gameOverAudio;
    public PlayerHealthController playerHealthController;

    private void Start() {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("GameOver");
        if(other.gameObject.CompareTag("Player")) {
            SendAnalytics3 ob = gameObject.AddComponent<SendAnalytics3>();
            Debug.Log("Player hit !!!!!!");
            if (gameObject.CompareTag("EnemyMonster"))
            {
                Debug.Log("Monster");
                ob.Send("Monster");
                // PlayerHealthController.instance.DealDamage();
            }

            else if (gameObject.CompareTag("FireBall"))
            {
                Debug.Log("Dropping Box");
                ob.Send("Dropping Box");
                // PlayerHealthController.instance.DealDamage();
            }
            else if (gameObject.CompareTag("Rotating Saw"))
            {
                Debug.Log("Rotating Saw");
                ob.Send("Rotating Saw");
            }
            PlayerHealthController.instance.DealDamage();
        }
    }
}
