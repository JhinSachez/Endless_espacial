using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataBase", menuName = "Scriptables/SoundDataBase", order = 0)]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] AudioData[] audioData;

    public AudioClip GetAudio(AUDIO_TYPE _audioType)
    {
        for (int i = 0; i < audioData.Length; i++)
        {
            if (audioData[i].audioType == _audioType)
            {
                return audioData[i].audio[UnityEngine.Random.Range(0, audioData[i].audio.Length)];
            }
        }

        return null;
    }
}

[Serializable]
public class AudioData
{
    public AudioClip[] audio;
    public AUDIO_TYPE audioType;
}

public enum AUDIO_TYPE
{
    FOOTSTEP,
    HIT,
    SHOOT
}