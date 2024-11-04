
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxSource2;

    public static AudioManager instance;
    public AudioClip levelMusic;
    public AudioClip footsteps;
    public AudioClip outsideFootsteps;
    public AudioClip swing;
    public AudioClip loot;
    bool sfxPlaying;
    bool sfxPlaying2;
    bool musicPlaying;
    public PlayerIUiOpener playerIUiOpener;
  


    public void PlaySfx(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }
    public void LootSfx()
    {
        if (!sfxPlaying2)
        {


            sfxSource2.PlayOneShot(loot);
            sfxPlaying2 = true;
            StartCoroutine(WaitForSfxEnd2());





        }
    }
    public void Footsteps()
    {
        
        if (!sfxPlaying)
        {
            if (playerIUiOpener.lobby)
            {
                sfxSource.PlayOneShot(footsteps);
                sfxPlaying = true;
                StartCoroutine(WaitForSfxEnd());
            }
            else
            {
                sfxSource.PlayOneShot(outsideFootsteps);
                sfxPlaying = true;
                StartCoroutine(WaitForSfxEnd());
            }
           
        }
    }
    public void Swing()
    {

        if (!sfxPlaying2)
        {


                sfxSource2.PlayOneShot(swing);
                sfxPlaying2 = true;
                StartCoroutine(WaitForSfxEnd2());
            
           
           
            

        }
    }
    private void Update()
    {
        if (!playerIUiOpener.lobby && !musicPlaying)
        {
            musicPlaying = true;
            musicSource.clip = levelMusic;
            musicSource.Play();
            StartCoroutine(WaitForMusicEnd());
        }
        if (playerIUiOpener.lobby)
        {
            musicSource.Stop();
        }
        
    }
    private IEnumerator WaitForSfxEnd()
    {
        while (sfxSource.isPlaying)
        {
            yield return null; 
        }
        sfxPlaying = false;


    }
    private IEnumerator WaitForSfxEnd2()
    {
        while (sfxSource2.isPlaying)
        {
            yield return null;
        }
        sfxPlaying2 = false;


    }
    private IEnumerator WaitForMusicEnd()
    {
        while (musicSource.isPlaying)
        {
            yield return null;
        }
       musicPlaying = false;
    }
    private IEnumerator PlayLevelMusic()
    {
        yield return new WaitForSeconds(1.5f);
        
        musicSource.Play();
       
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
       



}
  
