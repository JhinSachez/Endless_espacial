using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Action<Game_State> OnGameStateChanged;  // Evento para cambiar el estado del juego
    public Game_State currentGameState = Game_State.Play;  // Estado actual del juego

    private const string monedasKey = "MonedasTotales";  // Clave para almacenar las monedas en PlayerPrefs

    private void Start()
    {
        CargarMonedas();  // Cargar las monedas almacenadas desde PlayerPrefs
    }

    public void ChangeGameState(Game_State _newGameState)
    {
        if (currentGameState == _newGameState) return;

        currentGameState = _newGameState;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    public int ObtenerMonedas()
    {
        return PlayerPrefs.GetInt(monedasKey, 0);  // Devuelve las monedas guardadas o 0 si no existen
    }

    public void AgregarMonedas(int cantidad)
    {
        int monedasActuales = ObtenerMonedas();
        monedasActuales += cantidad;
        PlayerPrefs.SetInt(monedasKey, monedasActuales);  // Guardar monedas actualizadas
        PlayerPrefs.Save();

        // Notificar a la UI
        MonedasTotalesUI ui = FindObjectOfType<MonedasTotalesUI>();
        if (ui != null)
        {
            ui.OnCoinsUpdated();
        }
    }

    private void CargarMonedas()
    {
        Debug.Log("Monedas cargadas: " + ObtenerMonedas());
    }

    public void ResetearMonedas()
    {
        PlayerPrefs.SetInt(monedasKey, 0);
        PlayerPrefs.Save();
    }
}

public enum Game_State
{
    Play,
    Pause,
    Game_Over
}

