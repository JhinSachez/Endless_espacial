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

    public void Algo()
    {

    }
    #endregion

    public Action<Game_State> OnGameStateChanged;
    public Game_State currentGameState;

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

}

public enum Game_State
{
    Play,
    Pause,
    Game_Over
}

