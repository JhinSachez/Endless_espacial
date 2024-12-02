using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;  

public class Coin : MonoBehaviour
{
    public static bool _magnetOn = false;  // Variable para el efecto de imán
    public float magnetSpeed = 5f;  // Velocidad del efecto de imán
    private GameObject _player;  // Referencia al jugador

    private bool isOnPlay;  // Controla si el juego está en ejecución
    private bool yaColisionado = false;  // Verifica si ya ha colisionado con el jugador


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");  // Encuentra al jugador por el tag
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;  // Se suscribe a los cambios de estado del juego
        OnGameStateChange(GameManager.GetInstance().currentGameState);  // Verifica el estado actual del juego
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;  // Solo se permite interactuar cuando el juego está en "Play"
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaColisionado)
        {
            yaColisionado = true;
            RecolectarMoneda();  // Manejar la recolección de la moneda
        }
        else if (other.CompareTag("InvisibleBarrier"))
        {
            // Desactivar la moneda al tocar la barrera invisible
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Enemigo"))
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isOnPlay) return;  // No ejecutar si no está en el estado de "Play"

        // Efecto de imán activo
        if (_magnetOn)
        {
            MagnetEffect();
        }
        
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 0.5f);  // Radio de búsqueda de 0.5 unidades

        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemigo"))
            {
                gameObject.SetActive(false);
                return;
            }
        }

    }

    private void RecolectarMoneda()
    {
        GameManager.GetInstance().AgregarMonedas(1);  // Añadir monedas al total

        // Actualizar la UI de monedas, si está disponible
        MonedasTotalesUI monedasTotalesUI = FindObjectOfType<MonedasTotalesUI>();
        if (monedasTotalesUI != null)
        {
            monedasTotalesUI.OnCoinsUpdated();
        }

        gameObject.SetActive(false);  // Desactivar la moneda
    }

    void MagnetEffect()
    {
        if (_player != null)
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, magnetSpeed * Time.deltaTime);

            // Si la moneda está lo suficientemente cerca del jugador, recolectarla automáticamente
            if (Vector3.Distance(transform.position, _player.transform.position) < 0.5f && !yaColisionado)
            {
                yaColisionado = true;
                RecolectarMoneda();
            }
        }
    }

    public void EndMagnetEffect()
    {
        _magnetOn = false;  // Finalizar el efecto de imán
    }
}
