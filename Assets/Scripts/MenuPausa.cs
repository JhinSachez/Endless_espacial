using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausas;
    bool isOnPlay;

    private void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }
    public void Pausa()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Pause);
        botonPausa.SetActive(false);
        menuPausas.SetActive(true);
    }

    public void Reanudar()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Play);
        botonPausa.SetActive(true);
        menuPausas.SetActive(false);
    }

    public void Reiniciar()
    {
        GameManager.GetInstance().ChangeGameState(Game_State.Play);
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
