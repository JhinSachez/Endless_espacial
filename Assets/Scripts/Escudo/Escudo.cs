using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    bool isOnPlay;
    public float Speed = 5f;
    public float Timer = 0;
    public float timeToDeactivated = 15;
    private bool yaColisionado = false;
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerShleld._isShieldOn = true;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isOnPlay) return;
        if (Timer > timeToDeactivated)
        {
            Timer = 0;
        }
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
        Timer += Time.deltaTime;

        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }
    }
}
