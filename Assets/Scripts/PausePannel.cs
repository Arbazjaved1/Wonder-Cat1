using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PausePannel : MonoBehaviour
{
    public GameObject pausepannel;
    public RectTransform pausepannelrect,pausebutton;
    public float topposy, middleposy;
    public float tweenduration;
    public CanvasGroup canvasgroup;
    public AudioManager audiomanager;
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pausepannel()
    {
        pausepannel.SetActive(true);
        Time.timeScale = 0f;
        Pausepannelintro();
    }
    public async void Resume()
    {
       await PausepannelOutro();
        pausepannel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RetryButton()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        //print(y);
        SceneManager.LoadScene(y + 0);
        Time.timeScale = 1f;
        //int level = SaveSystem.Load("currentlevel", 0);
    }
    void Pausepannelintro()
    {
        audiomanager.PlaySFX(audiomanager.Pausesound);
        canvasgroup.DOFade(1, tweenduration).SetUpdate(true);
        pausepannelrect.DOAnchorPosY(middleposy, tweenduration).SetUpdate(true);
        pausebutton.DOAnchorPosX(150, tweenduration).SetUpdate(true);
    }
    async Task PausepannelOutro()
    {
        audiomanager.PlaySFX(audiomanager.Pausesound);
        canvasgroup.DOFade(0, tweenduration).SetUpdate(true);
        await pausepannelrect.DOAnchorPosY(topposy, tweenduration).SetUpdate(true).AsyncWaitForCompletion();
        pausebutton.DOAnchorPosX(-15, tweenduration).SetUpdate(true);
    }
}
