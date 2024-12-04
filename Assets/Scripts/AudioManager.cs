using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicsorce;
    public AudioSource sfxsorce;

    public AudioClip background;
    public AudioClip death;
    public AudioClip coins;
    public AudioClip Pausesound;
    // Start is called before the first frame update
    void Start()
    {
        musicsorce.clip = background;
        musicsorce.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(AudioClip Clip)
    {
        sfxsorce.PlayOneShot(Clip);
    }
}
