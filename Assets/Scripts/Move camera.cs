using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecamera : MonoBehaviour
{
    bool isOnPlay;
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3); 
    }
    
    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnPlay) return;
    }
}
