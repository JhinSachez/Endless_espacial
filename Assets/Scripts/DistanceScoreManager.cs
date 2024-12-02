using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScoreManager : MonoBehaviour
{
    public static DistanceScoreManager instance; // Instancia estática para acceso global
    public DistanceScore _distanceScore; // Referencia única a DistanceScore

    void Start()
    {
        // Buscar el objeto "Player" y asignar el componente DistanceScore en Start
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log($"Buscando el componente DistanceScore en: {player.name}");

            // Intentar obtener el componente DistanceScore del objeto encontrado
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
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player'.");
        }
    }
}
