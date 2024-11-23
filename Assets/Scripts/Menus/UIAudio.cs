using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.GetInstance().SetAudioWithPosition(AUDIO_TYPE.SHOOT, transform.position);
        Debug.Log("audio");
    }
}
