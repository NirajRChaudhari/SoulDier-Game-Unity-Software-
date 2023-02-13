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
            Debug.Log("Player dead !!!!!!");
            PlayerHealthController.instance.DealDamage();
        }
    }
}
