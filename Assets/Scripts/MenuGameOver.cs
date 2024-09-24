using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{

    [SerializeField] private GameObject menuGameOver;
    private CombateJugador Vida;
    bool isOnGameOver;

    private void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);

        Vida = GameObject.FindGameObjectWithTag("Player").GetComponent<CombateJugador>();
        // Vida.MuerteJugador += ActivarMenu;
    }

    void OnGameStateChange(Game_State _gs)
    {
        if (_gs == Game_State.Game_Over)
        {
            menuGameOver.SetActive(true);
        }
    }

    public void ActivarMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }

    public void Reinciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void Update()
    {
        if (!isOnGameOver) return;
    }
}
