using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitch : MonoBehaviour
{
    public LevelManager levelManager;

    public int currentlevel;

    public PlayerMovements playerController;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveSystem.Save("level" + levelManager.currentlevel + "Pickup", playerController.count);
            FindObjectOfType<UIManager>().Levelcomplete();
            if (levelManager.currentlevel < 9)
                levelManager.currentlevel++;
            else
                levelManager.currentlevel = 0;
            SaveSystem.Save("level" + levelManager.currentlevel + "unlock", other.GetComponent<Cat>().levelManager);
            SaveSystem.Save("currentlevel", levelManager.currentlevel);
            SaveSystem.Save("Pickup", other.GetComponent<Cat>().count);
        }
    }
    public void Currentlevel()
    {
        SaveSystem.Save("currentlevel", 0);
        currentlevel = SaveSystem.Load("currentlevel", 0);
    }
}
