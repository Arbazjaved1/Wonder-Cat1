using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;        // Array of level buttons
    public GameObject lockPrefab;  // Lock icon prefab

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Initialize all buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                buttons[i].interactable = true; // Unlock button
            }
            else
            {
                buttons[i].interactable = false; // Lock button
                AddLockIcon(buttons[i]);        // Add lock icon
            }
        }
    }

    // Add a lock icon to a button
    private void AddLockIcon(Button button)
    {
        GameObject lockIcon = Instantiate(lockPrefab, button.transform);
        lockIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the lock icon
    }

    // Open the selected level
    public void openLevel(int LevelId)
    {
        string LevelName = "level " + LevelId;

        if (Application.CanStreamedLevelBeLoaded(LevelName))
        {
            SceneManager.LoadScene(LevelName);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError($"Scene '{LevelName}' not found. Check your build settings.");
        }
    }

    // Unlock the next level
    public void UnlockNextLevel(int completedLevelId)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Ensure only one level is unlocked
        if (completedLevelId == unlockedLevel && completedLevelId < buttons.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
