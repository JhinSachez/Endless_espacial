using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vaca : MonoBehaviour
{
    #region ruben iman
    public static bool _magnetOn = false;
    public float magnetSpeed;
    private GameObject _player;
    #endregion

    #region paco
    public float Speed = 200.0f;
    bool isOnPlay;
    public float Timer = 0;
    public float timeToDeactivated = 15;
    private bool yaColisionado = false;
    #endregion

    void Start()
    {
        // se busca al jugador 
        _player = GameObject.FindGameObjectWithTag("Player");

        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaColisionado)
        {
            yaColisionado = true;

            AudioManager.instance.PlayMoneda();

            // se llama la funcion para aplicar el efecto del iman
            MagnetEffect(); 

            // Aquí normalmente agregarías puntos al jugador, por ejemplo:
            GameManager.GetInstance().SumarPuntos(1);

            // En lugar de destruir el objeto, simplemente lo desactivamos
            gameObject.SetActive(false);


        }
    }

    #region funciones para iman
    void MagnetEffect()
    {
        
        transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, magnetSpeed * Time.deltaTime);


        Invoke("EndEffect", 5f);

    }

    void EndEffect()
    {
        _magnetOn = false;
    }
    #endregion

    void Update()
    {
        if (!isOnPlay) return;
        // para efecto del iman, solo si esta activado
        if (_magnetOn)
        {
            MagnetEffect();
        }
        // ---

        if (Timer > timeToDeactivated)
        {
            Timer = 0;
        }

        

        transform.Translate(Vector3.back * Speed * Time.deltaTime);
        Timer += Time.deltaTime;

        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }
    }
}
