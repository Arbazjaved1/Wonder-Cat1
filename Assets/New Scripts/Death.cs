using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Animator anim;
    private AudioManager audiomanager;

    // References to UI panels
    public GameObject gameplayPanel;
    public GameObject gameOverPanel;

    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Ensure initial states of the panels
        if (gameplayPanel != null) gameplayPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
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
                        script.enabled = false;  // Disable all scripts except this one
                    }
                }

                playerHealth.TakeDamage(playerHealth.maxHealth); // Apply fatal damage

                // Show GameOver panel and hide Gameplay panel
                ShowGameOverPanel();
            }
        }
    }

    private void ShowGameOverPanel()
    {
        if (gameplayPanel != null && gameOverPanel != null)
        {
            gameplayPanel.SetActive(false); // Hide Gameplay panel
            gameOverPanel.SetActive(true);  // Show GameOver panel
        }
        else
        {
            Debug.LogError("GameOverPanel or GameplayPanel is not assigned!");
        }
    }
    public void GoToMainMenu()
    {
        FindAnyObjectByType<Cat>().ResetSceneCoins();
        SceneManager.LoadScene("MainMenu 1"); // Replace "MainMenu" with the exact name of your main menu scene
    }
}
