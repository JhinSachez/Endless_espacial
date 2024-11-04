using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Clips to play")]
    [SerializeField] AudioClip musicClip; // bgm
    [SerializeField] AudioClip sfxClip;
    [SerializeField] AudioClip CoinClip; 
                                        
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlayEffect()
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlayCoin()
    {
        sfxSource.PlayOneShot(CoinClip);
    }
}