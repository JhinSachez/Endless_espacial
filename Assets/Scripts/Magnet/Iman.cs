using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
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
            Coin._magnetOn = true;
            Destroy(gameObject); 
        }
    }


    void Update()
    {
        if (!isOnPlay) return;

        transform.Translate(Vector3.back * 5f * Time.deltaTime);
    }
}
