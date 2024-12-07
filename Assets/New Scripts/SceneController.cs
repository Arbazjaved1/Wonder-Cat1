using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator Transition;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator LoadLevel(int LevelIndex)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelIndex);
    }
}
