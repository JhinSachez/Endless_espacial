using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MenuGameOver : MonoBehaviour
{

    [SerializeField] private GameObject menuGameOver;
    private CombateJugador Vida;
    bool isOnGameOver;
    public DistanceScore distanceScore;
    public TextMeshProUGUI distanceText;
    
    public TextMeshProUGUI vacaText;   

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
            distanceText.text = distanceScore.scoreText.text;
            vacaText.text = GameManager.GetInstance().ObtenerPuntuacion().ToString();
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

    private void Update()
    {
        if (!isOnGameOver) return;
    }
}
