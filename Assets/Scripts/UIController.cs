using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion; // Referencia al componente TextMeshPro en la UI

    // Método para actualizar el TextMeshPro con la puntuación actual
    public void ActualizarPuntuacion(int puntos)
    {
        textoPuntuacion.text = "Puntos: " + GameManager.GetInstance().ObtenerPuntuacion().ToString();
    }
    
    private void Update()
    {
        ActualizarPuntuacion(1);
    }
}
