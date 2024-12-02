using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsE : MonoBehaviour
{
       bool isOnPlay;
    private bool yaColisionado = false; // Para rastrear si ya colisionó con el jugador
    public DistanceScore _distanceScore;

    // Referencia al script de controlAparicion
    public controlAparicion controlAparicion;

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);

        // Busca automáticamente el componente DistanceScore al inicio si no está asignado
        if (_distanceScore == null)
        {
            FindDistanceScore();
        }
    }

    private void Update()
    {
        // Verificamos si isOnSpace es true, si es así, activamos el objeto, si no, lo desactivamos
        if (controlAparicion != null)
        {
            if (controlAparicion.isOnSpace)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        // Si el DistanceScore es válido y la distancia supera 20,000, podemos modificar lo que queramos
        if (_distanceScore != null && _distanceScore.distance >= 20000)
        {
            // Aquí puedes colocar la lógica adicional si es necesario cuando la distancia sea mayor a 20,000
        }
    }

    void OnEnable()
    {
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
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("InvisibleBarrier"))
        {
            gameObject.SetActive(false);
        }
    }

    // Método para buscar y asignar automáticamente el componente DistanceScore
    private void FindDistanceScore()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _distanceScore = player.GetComponent<DistanceScore>();
            if (_distanceScore != null)
            {
                Debug.Log($"DistanceScore asignado correctamente al objeto: {player.name}");
            }
            else
            {
                Debug.LogError($"El objeto '{player.name}' tiene la etiqueta 'Player', pero no contiene el componente DistanceScore.");
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto etiquetado como 'Player' en la escena.");
        }
    }
}
