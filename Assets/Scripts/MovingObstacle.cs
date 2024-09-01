using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 direction = Vector3.left;  // Dirección del movimiento (por defecto a la izquierda)
    public float speed = 5f;                  // Velocidad del movimiento
    public float lifetime = 10f;              // Tiempo de vida del objeto antes de ser destruido

    void Start()
    {
        // Destruye el objeto después de un tiempo determinado
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve el objeto en la dirección especificada a la velocidad determinada
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
