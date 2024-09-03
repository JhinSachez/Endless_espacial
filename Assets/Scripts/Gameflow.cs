using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameflow : MonoBehaviour
{
    public Transform tile1Obj;
    private Vector3 nextTileSpawn;
    void Start()
    {
        nextTileSpawn.z = 50;
        StartCoroutine(SpawnTile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(3);
        Instantiate(tile1Obj, nextTileSpawn, tile1Obj.rotation);
        nextTileSpawn.z += 10;
        StartCoroutine(SpawnTile());
    }
}
