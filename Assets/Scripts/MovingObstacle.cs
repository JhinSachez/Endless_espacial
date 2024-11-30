using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    bool isOnPlay;
    private bool yaColisionado = false; // Para rastrear si ya colisionó con el jugador

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }
    
    void OnEnable()
    {
        // Reiniciar el estado cuando el objeto se activa
        yaColisionado = false;
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

            // Desactivar el objeto después de la colisión con el jugador
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("InvisibleBarrier"))
        {
            // Desactivar el objeto si toca la barrera invisible
            gameObject.SetActive(false);
        }
    }
}
