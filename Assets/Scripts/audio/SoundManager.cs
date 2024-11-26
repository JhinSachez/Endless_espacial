using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    private static SoundManager _instance;

    public static SoundManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        selfAudioSource = GetComponent<AudioSource>();
    }

    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("sfx_vol"))
        {
            currentVol = PlayerPrefs.GetFloat("sfx_vol", currentVol);
            mixer.SetFloat("sfx_vol", currentVol);
        }
    }

    [SerializeField] SoundDataBase soundDataBase;
    AudioSource selfAudioSource;

    [SerializeField] GameObject audiosourcePrefab;
    List<AudioSource> audioSourcesCreated = new List<AudioSource>();

    public void SetAudio(AUDIO_TYPE _audioToPlay)
    {
        selfAudioSource.PlayOneShot(soundDataBase.GetAudio(_audioToPlay));
    }

    public void SetAudioWithPosition(AUDIO_TYPE _audioToPlay, Vector3 _pos)
    {
        AudioSource localAudioSource = GetAudioSource();
        localAudioSource.clip = soundDataBase.GetAudio(_audioToPlay);
        localAudioSource.transform.position = _pos;
        localAudioSource.Stop();
        localAudioSource.Play();
        Debug.Log("audio monedas");
    }

    public AudioSource GetAudioSource()
    {
        for (int i = 0; i > audioSourcesCreated.Count; i++)
        {
            if (!audioSourcesCreated[i].isPlaying)
            {
                return audioSourcesCreated[i];
            }
        }
        AudioSource newAudioSource = Instantiate(audiosourcePrefab).GetComponent<AudioSource>();
        audioSourcesCreated.Add(newAudioSource);
        return newAudioSource;
    }

    [SerializeField] AudioMixer mixer;
    float currentVol = 0;

    void ChangeVolume(bool volUp = true)
    {
        currentVol = volUp ? Mathf.Max(-80, currentVol - 20) : Mathf.Min(0, currentVol + 20);
        mixer.SetFloat("sfx_vol", currentVol);
        PlayerPrefs.SetFloat("sfx_vol", currentVol);
    }

    public void PlaySoundFX(AudioClip audioClip, Transform Spawn)
    {
        AudioSource audioSource = Instantiate(selfAudioSource, Spawn.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) //subir volumen
        {
            ChangeVolume(false);
        }
        else if (Input.GetKeyDown(KeyCode.V)) //bajar volumen
        {
            ChangeVolume();
        }
    }*/

}