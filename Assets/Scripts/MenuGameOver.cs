using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{

    [SerializeField] private GameObject menuGameOver;
    private CombateJugador Vida;

    private void Start()
    {
        Vida = GameObject.FindGameObjectWithTag("Player").GetComponent<CombateJugador>();
        Vida.MuerteJugador += ActivarMenu;
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
}
