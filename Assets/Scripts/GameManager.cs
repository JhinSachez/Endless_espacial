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

    public Action<Game_State> OnGameStateChanged;
    public Game_State currentGameState = Game_State.Play;
    public int puntosTotales = 0;
    
    public void ChangeGameState(Game_State _newGameState)
    {
        if (currentGameState == _newGameState) return;

        currentGameState = _newGameState;
        Debug.Log("Game state changed to:" + currentGameState);
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged.Invoke(currentGameState);
        }
    }
    public void SumarPuntos(int puntos)
    {
        puntosTotales += puntos;
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeGameState(Game_State.Play);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
           ChangeGameState(Game_State.Pause);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeGameState(Game_State.Game_Over);
        }
    }

    // Método para obtener la puntuación actual
    public int ObtenerPuntuacion()
    {
        return puntosTotales;
    }
}

public enum Game_State
{
    Play,
    Pause,
    Game_Over
}

