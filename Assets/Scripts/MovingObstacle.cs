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
    private bool yaColisionado = false;              // Tiempo de vida del objeto antes de ser destruido

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

        transform.Translate(Vector3.back * speed * Time.deltaTime);
        Timer += Time.deltaTime;

        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }
    }
}
