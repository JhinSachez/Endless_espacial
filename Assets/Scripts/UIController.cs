using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
 /*   public TextMeshProUGUI textoPuntuacion; // Referencia al componente TextMeshPro en la UI
    public GameObject texto; // Texto que muestra la puntuación actual

    private void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        textoPuntuacion = texto.GetComponent<TextMeshProUGUI>();
    }

    // Método para actualizar el TextMeshPro con la puntuación actual
    public void ActualizarPuntuacion()
    {
        textoPuntuacion.text = "Vacas: " + GameManager.GetInstance().ObtenerPuntuacion().ToString();
    }

    void OnGameStateChange(Game_State _gs)
    {
        if (_gs == Game_State.Game_Over)
        {
            texto.SetActive(false); // Oculta el texto de la puntuación al finalizar el juego
        }
    }

    private void Update()
    {
        ActualizarPuntuacion(); // Actualiza constantemente la puntuación en pantalla
    }
    */
}
