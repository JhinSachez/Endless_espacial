using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static bool _magnetOn = false;  // Variable para el efecto de imán
    public float magnetSpeed = 5f;  // Velocidad del efecto de imán
    private GameObject _player;  // Referencia al jugador

    public float Speed = 5f;  // Velocidad de movimiento de la moneda
    private bool isOnPlay;  // Controla si el juego está en ejecución
    private float Timer = 0;  // Temporizador para la desactivación de la moneda
    public float timeToDeactivated = 15f;  // Tiempo antes de desactivar la moneda
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
    }

    void Update()
    {
        if (!isOnPlay) return;  // No ejecutar si no está en el estado de "Play"

        // Efecto de imán activo
        if (_magnetOn)
        {
            MagnetEffect();
        }

        // Temporizador para desactivar la moneda automáticamente
        Timer += Time.deltaTime;
        if (Timer > timeToDeactivated)
        {
            Timer = 0;
            gameObject.SetActive(false);
        }

        // Movimiento de la moneda hacia atrás
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
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
