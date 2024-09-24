using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public static bool _magnetOn = false;
    public float magnetSpeed;
    private GameObject _player;

    #region paco
    public float Speed = 200.0f;
    bool isOnPlay;
    public float Timer = 0;
    public float timeToDeactivated = 15;
    private bool yaColisionado = false;
    #endregion

    void Start()
    {
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

            MagnetEffect(); 

            // Aquí normalmente agregarías puntos al jugador, por ejemplo:
            GameManager.GetInstance().SumarPuntos(1);

            // En lugar de destruir el objeto, simplemente lo desactivamos
            gameObject.SetActive(false);


        }
    }

    void MagnetEffect()
    {
        transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, magnetSpeed * Time.deltaTime);
        Invoke("EndEffect", 10f);
    }
       
    void EndEffect()
    {
        _magnetOn = false;
    }  

    void Update()
    {

        if (!isOnPlay) return;
        if (_magnetOn)
        {
            MagnetEffect();
        }
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
