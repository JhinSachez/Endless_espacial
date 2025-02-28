using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    bool isOnPlay;
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShleld._isShieldOn = true;
            gameObject.SetActive(false);
        }

        Debug.Log("escudo activo");
    }

    void Update()
    {
        if (!isOnPlay) return;
        transform.Translate(Vector3.back * 5f * Time.deltaTime);
    }
}
