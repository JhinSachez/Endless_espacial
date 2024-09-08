using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausas;
    bool isOnPlay;

    private bool juegoPausado = false;

    private void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }
    public void Pausa()
    {
        juegoPausado = true;
        //GameManager.GetInstance().ChangeGameState(Game_State.Pause);
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausas.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado = false;
        //GameManager.GetInstance().ChangeGameState(Game_State.Play);
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausas.SetActive(false);
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 0f;
        //GameManager.GetInstance().ChangeGameState(Game_State.Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar ()
    {
        Debug.Log("cerrando juego");
        Application.Quit();
    }

    void OnGameStateChanged(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }
}
