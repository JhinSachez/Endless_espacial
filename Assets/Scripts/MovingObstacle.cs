using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    bool isOnPlay;
    public Vector3 direction = Vector3.left;  // Dirección del movimiento (por defecto a la izquierda)
    public float speed = 5f;                  // Velocidad del movimiento
    public float Timer = 0;
    public float timeToDeactivated = 5;
    private bool yaColisionado = false;       // Para rastrear si ya colisionó con el jugador

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }
    
    void OnEnable()
    {
        // Reiniciar el estado cuando el objeto se activa
        Timer = 0;
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

            // Desactivar el objeto después de la colisión
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isOnPlay) return;

        // Mover el obstáculo en la dirección especificada
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Incrementar el temporizador
        Timer += Time.deltaTime;

        // Desactivar el objeto si el temporizador excede el tiempo definido
        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }
    }
    
}
