using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool test;
    public int currentlevel;
    public GameObject[] levels;
    public GameObject[] levelexit;
    private void Start()
    {
        if (test)
        {
            levels[currentlevel].SetActive(true);
            return;
        }

        if (!PlayerPrefs.HasKey("currentlevel"))
        {
            PlayerPrefs.SetInt("currentlevel", 0);
            currentlevel = 0;
        }
        else
        {
            currentlevel = PlayerPrefs.GetInt("currentlevel");
        }
        //currentlevel -= 1;
        levels[currentlevel].SetActive(true);

    }
}

