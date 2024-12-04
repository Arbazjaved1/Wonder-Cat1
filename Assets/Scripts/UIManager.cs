using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject Gameover;

    public GameObject ShowControls;

    public GameObject MainMenu;

    public RectTransform gameover;

    public float topposy, middleposy;

    public float tweenDuration;

    public CanvasGroup canvasgroup;

    public GameObject LevelComplete;

    public RectTransform levelcomp;

    public float topposy1, middleposy1;

    public float tweenDuration1;

    public CanvasGroup canvasgroup1;

    public LevelManager levelmanager;

    //public TextMeshProUGUI coinstext;

    public GameObject optionPannel;

    public GameObject Levelselection;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void LevelSelection()
    {
        Levelselection.SetActive(true);
        MainMenu.SetActive(false);
        ShowControls.SetActive(false);
    }
    public void Levelcomplete()
    {
        LevelComplete.SetActive(true);
        Gameover.SetActive(false);
        ShowControls.SetActive(false);
        levelcompleteintro();
    }
    public void OptionPannel()
    {
        optionPannel.SetActive(true);
        ShowControls.SetActive(false);
        MainMenu.SetActive(false);
    }
    public void GameOver()
    {
        Gameover.SetActive(true);
        ShowControls.SetActive(false);
        gameoverpannelintro();

    }
    public void RetryButton()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        print(y);
        SceneManager.LoadScene(y + 0);
        //int level = SaveSystem.Load("currentlevel", 0);
    }
    public void Showcontrols()
    {
        ShowControls.SetActive(true);
        MainMenu.SetActive(false);
        FindObjectOfType<Cat>().startlevel();
        //coinstext.text = "  " + SaveSystem.Load("Pickup", 0);
    }
    void gameoverpannelintro()
    {
        canvasgroup.DOFade(1, tweenDuration).SetUpdate(true);
        gameover.DOAnchorPosY(middleposy, tweenDuration);
    }
    void levelcompleteintro()
    {
        canvasgroup.DOFade(1, tweenDuration).SetUpdate(true);
        levelcomp.DOAnchorPosY(middleposy, tweenDuration);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
