using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool hasTriggered = false;

    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Main menu scene name
    [SerializeField] private AudioClip finishSound; // Optional finish sound
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Ensure this is triggered only once
            UnlockNewLevel();
            StartCoroutine(LoadNextLevel());
        }
    }

    void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);

        if (currentSceneIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", Mathf.Max(PlayerPrefs.GetInt("UnlockedLevel", 1), currentSceneIndex + 1));
            PlayerPrefs.Save();
        }
    }

    IEnumerator LoadNextLevel()
    {
        // Call SaveCoinsOnLevelComplete to save coins when the level is completed
        GameObject.FindObjectOfType<Cat>().SaveCoinsOnLevelComplete();

        // Optionally play a finish sound
        if (finishSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(finishSound);
            yield return new WaitForSeconds(finishSound.length);
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName); // Replace with actual menu scene name
        }
    }
}
