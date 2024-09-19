using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;

public class Vaca : MonoBehaviour
{
    public float Speed = 200.0f;
    bool isOnPlay;
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
        if (other.CompareTag("Player") && !yaColisionado)
        {
            yaColisionado = true;

            // Aquí normalmente agregarías puntos al jugador, por ejemplo:
            GameManager.GetInstance().SumarPuntos(1);

            // En lugar de destruir el objeto, simplemente lo desactivamos
            gameObject.SetActive(false);
        }
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
