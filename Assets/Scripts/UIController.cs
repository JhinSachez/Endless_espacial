using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion; // Referencia al componente TextMeshPro en la UI
    public GameObject texto;

    private void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        textoPuntuacion = texto.GetComponent<TextMeshProUGUI>();
    }

    // Método para actualizar el TextMeshPro con la puntuación actual
    public void ActualizarPuntuacion(int puntos)
    {
        textoPuntuacion.text = "Puntos: " + GameManager.GetInstance().ObtenerPuntuacion().ToString();
    }
    
    void OnGameStateChange(Game_State _gs)
    {
        if (_gs == Game_State.Game_Over)
        {
                texto.SetActive(false);
        }
    }
    private void Update()
    {
        ActualizarPuntuacion(1);
    }
    
}
