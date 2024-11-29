using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public void PlaySound()
    {
        //if(SoundManager.GetInstance().GetAudioSource() == null) return;
        SoundManager.GetInstance().SetAudio(AUDIO_TYPE.SHOOT);
        Debug.Log("audio");
    }
}
