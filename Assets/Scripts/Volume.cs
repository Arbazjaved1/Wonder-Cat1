using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mymixer;
    public Slider musicslider;
    public Slider sfxslider;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolum"))
        {
            Loadvolum();
        }
        else
        {
            SetMusicVolum();
            SetSFXVolum();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicVolum()
    {
        float volume = musicslider.value;
        mymixer.SetFloat("Music", Mathf.Log10(volume) *20);
        PlayerPrefs.SetFloat("MusicVolum", volume);
    }
    public void SetSFXVolum()
    {
        float volume = sfxslider.value;
        mymixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolum", volume);
    }
    private void Loadvolum()
    {
        musicslider.value = PlayerPrefs.GetFloat("MusicVolum");
        sfxslider.value = PlayerPrefs.GetFloat("SFXVolum");
        SetMusicVolum();
        SetSFXVolum();
    }
}
