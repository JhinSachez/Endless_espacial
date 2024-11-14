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
    [SerializeField] AudioClip musicClip; 
    [SerializeField] AudioClip sfxClip;
    [SerializeField] AudioClip MonedaClip;
    [SerializeField] AudioClip PowerUp;
    [SerializeField] AudioClip GameOver;


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

    public void PlayMoneda()
    {
        sfxSource.PlayOneShot(MonedaClip);
    }

    public void PlayPowerUp()
    {
        sfxSource.PlayOneShot(PowerUp);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(GameOver);
    }
}