using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] private bool sfxisOn = true;
    public Image sfxOn;
    public Image sfxOff;

    [SerializeField] private bool bgmIsOn = true;
    public Image bgmOn;
    public Image bgmOff;

    private const float muteVolume = -80f;
    private const float fullVolume = 0f;

    private void Start()
    {
        LoadAudioSettings();
        UpdateButtonStates();
    }

    public void ToggleSFX()
    {
        sfxisOn = !sfxisOn;
        float newVolume = sfxisOn ? fullVolume : muteVolume;
        mixer.SetFloat("sfxVol", newVolume);

        PlayerPrefs.SetFloat("sfxVol", newVolume);
        PlayerPrefs.Save();

        UpdateButtonStates();
    }

    public void ToggleBGM()
    {
        bgmIsOn = !bgmIsOn;
        float newVolume = bgmIsOn ? fullVolume : muteVolume;
        mixer.SetFloat("musicVol", newVolume);

        PlayerPrefs.SetFloat("musicVol", newVolume);
        PlayerPrefs.Save();

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        sfxisOn = PlayerPrefs.GetFloat("sfxVol", fullVolume) != muteVolume;
        sfxOn.gameObject.SetActive(sfxisOn);
        sfxOff.gameObject.SetActive(!sfxisOn);

        bgmIsOn = PlayerPrefs.GetFloat("musicVol", fullVolume) != muteVolume;
        bgmOn.gameObject.SetActive(bgmIsOn);
        bgmOff.gameObject.SetActive(!bgmIsOn);
    }

    private void LoadAudioSettings()
    {
        float sfxVol = PlayerPrefs.GetFloat("sfxVol", fullVolume);
        float musicVol = PlayerPrefs.GetFloat("musicVol", fullVolume);

        mixer.SetFloat("sfxVol", sfxVol);
        mixer.SetFloat("musicVol", musicVol);
    }
}