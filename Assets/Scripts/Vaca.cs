using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;

public class Vaca : MonoBehaviour
{
    public float Speed = 1.0f;
    bool isOnPlay;
    public float Timer = 0;
    public float timeToDeactivated = 1;
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }


    void Update()
    {
        if (Timer > timeToDeactivated)
        {
            Timer = 0;
        }

        if (!isOnPlay) return;

        transform.Translate(Vector3.back * Speed * Time.deltaTime);
        Timer += Time.deltaTime;

        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }

    }
}
