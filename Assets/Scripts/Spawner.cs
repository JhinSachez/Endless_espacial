using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;      // Array de prefabs a instanciar
    public float minSpawnInterval = 1f;      // Intervalo mínimo de tiempo entre spawns
    public float maxSpawnInterval = 3f;      // Intervalo máximo de tiempo entre spawns

    void Start()
    {
        // Inicia la Coroutine para spawnear obstáculos
        StartCoroutine(SpawnObstacles());
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
