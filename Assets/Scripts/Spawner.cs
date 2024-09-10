using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool isOnPlay;
    public GameObject[] prefabsToSpawn;      // Array de prefabs a instanciar
    public float minSpawnInterval = 1f;      // Intervalo mínimo de tiempo entre spawns
    public float maxSpawnInterval = 6f;      // Intervalo máximo de tiempo entre spawns


    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        StartCoroutine(SpawnObstacles());

    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }

    private void Update()
    {
        if (!isOnPlay) return;


    }

    IEnumerator SpawnObstacles()
    {

        while (true)
        {
            // Escoge un prefab aleatoriamente del array
            GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

            // Instancia el objeto en la posición exacta del GameObject
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            // Espera un tiempo aleatorio antes del siguiente spawn
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);


        }
    }

  
    

}
