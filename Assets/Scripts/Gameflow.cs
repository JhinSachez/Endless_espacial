using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameflow : MonoBehaviour
{
    public Transform tile1Obj;
    private Vector3 nextTileSpawn;
    bool isOnPlay;
    public float timer = 1;
    void Start()
    {
        nextTileSpawn.z = 50;
       
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }
    
    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnPlay) return;
        
        SpawnTile();
    }

    private void SpawnTile()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
        Instantiate(tile1Obj, nextTileSpawn, tile1Obj.rotation);
        nextTileSpawn.z += 10;
        timer = 1;
        }
    }
}
