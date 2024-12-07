using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null && !playerHealth.IsDead())
            {
                Debug.Log("Player detected!");

                anim = other.GetComponent<Animator>();  // Get the Animator component from the player

                if (anim != null)
                {
                    anim.SetTrigger("Die");  // Play the death animation
                    //FindObjectOfType<UIManager>().GameOver();
                    audiomanager.PlaySFX(audiomanager.death);
                }
                else
                {
                    Debug.LogError("Animator component not found on player!");
                }

                Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.velocity = Vector2.zero;  // Stop player movement
                }

                MonoBehaviour[] playerScripts = other.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in playerScripts)
                {
                    if (script != this)
                    {
                        script.enabled = false;  // Disable all
                    }
                }

                playerHealth.TakeDamage(playerHealth.maxHealth); // Apply fatal damage
            }
        }
    }
}