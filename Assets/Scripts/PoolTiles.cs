using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PoolTiles : MonoBehaviour
{
    List<GameObject> tiles = new List<GameObject>();
   public List<GameObject> tileprefab = new List<GameObject>();
    bool isOnPlay;
    public float DistanciaSpawn;
    public Vector3 nextpos;
    public GameObject padre;
    public int TileLoopCount = 1;

    GameObject player;
    

    GameObject ObtenerTile()
    {
        foreach (GameObject tile in tiles)
        {
            if(tile.activeSelf == false)
            {
                return tile;
            }
        }

        GameObject newTile = Instantiate(tileprefab[Random.Range(0,tileprefab.Count)], Vector3.zero, Quaternion.Euler(new Vector3(0,-90,0)));
        tiles.Add(newTile);
        return newTile;
    }

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
        player = GameObject.FindGameObjectWithTag("Player");
        TileLoopCount = 1;

        for(int i = 0; i < tileprefab.Count; i++)
        {
            GameObject newTile = Instantiate(tileprefab[i], Vector3.zero, Quaternion.Euler(new Vector3(0,-90,0)));
            tiles.Add(newTile);
            newTile.transform.parent = padre.transform;
            newTile.SetActive(false);
        }

    }

    void OnGameStateChanged(Game_State _gameState)
    {
        isOnPlay = _gameState == Game_State.Play;

    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlay)
        {
    
           
           
            if(player.transform.position.z >= DistanciaSpawn* TileLoopCount)
            {
                TileLoopCount++;
               GameObject tile = ObtenerTile();
                tile.transform.position = nextpos;
                tile.SetActive(true);
                nextpos += new Vector3(0, 0, DistanciaSpawn);
                if(tiles.Count > 15)
                {
                    GameObject paco = tiles[0];
                    paco.SetActive(false);
                    tiles.Remove(paco);
                    tiles.Add(paco);
                }
            }
        }
    }
}
