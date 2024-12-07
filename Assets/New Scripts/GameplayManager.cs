using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject gameplayPanel; // Reference to the gameplay panel in the scene

    private void Start()
    {
        // Activate the Gameplay Panel when the level starts
        if (gameplayPanel != null)
        {
            gameplayPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Gameplay Panel is not assigned in GameplayManager!");
        }
    }

}
