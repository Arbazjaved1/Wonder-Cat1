using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedlevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openLevel(int LevelId)
    {
        string LevelName = "level " + LevelId;
        SceneManager.LoadScene(LevelName);
        Time.timeScale = 1f;
    }
}
