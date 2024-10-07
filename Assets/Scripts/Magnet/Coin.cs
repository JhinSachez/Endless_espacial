using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static bool _magnetOn = false;
    public float magnetSpeed;
    private GameObject _player;

    public float Speed = 5f;
    private bool isOnPlay;
    private float Timer = 0;
    public float timeToDeactivated = 15;
    private bool yaColisionado = false;

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
            GameManager.GetInstance().SumarPuntos(1);  // Sumar puntos al jugador
            gameObject.SetActive(false);  // Desactivar moneda
        }
    }

    void Update()
    {
        if (!isOnPlay) return;

        if (_magnetOn)
        {
            MagnetEffect();
        }

        Timer += Time.deltaTime;
        if (Timer > timeToDeactivated)
        {
            Timer = 0;
            gameObject.SetActive(false);  // Desactivar moneda después de cierto tiempo
        }

        // Movimiento de la moneda
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }

    void MagnetEffect()
    {
        transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, magnetSpeed * Time.deltaTime);
    }
}
