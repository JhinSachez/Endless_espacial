using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplePoolSpawner : MonoBehaviour
{
    bool isOnPlay;
    public string grupo = "GrupoMultiple";
    public List<ObjectPool> pools;  // Lista de diferentes pools
    public float minSpawnInterval = 1f;  // Intervalo mínimo entre spawns
    public float maxSpawnInterval = 3f;  // Intervalo máximo entre spawns
    public bool randomizePools = true;   // Si se debe elegir el pool de forma aleatoria

    void Start()
    {
        // Suscribir al evento de cambio de estado del juego
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        
        // Iniciar la corrutina solo si el juego está en estado "Play"
        if (isOnPlay)
        {
            StartCoroutine(SpawnObjects());
        }
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
        
        // Si el juego está en pausa o terminado, detener la corrutina de spawner
        if (!isOnPlay)
        {
            StopCoroutine(SpawnObjects());
        }
        else
        {
            StartCoroutine(SpawnObjects());
        }
    }

    private void Update()
    {
        if (!isOnPlay) return;
    }

    IEnumerator SpawnObjects()
    {
        while (isOnPlay)  // Solo generar objetos si el juego está en estado "Play"
        {
            // Elegir un pool al azar si randomizePools está activado, o según una lógica
            ObjectPool selectedPool = randomizePools ? GetRandomPool() : SelectPoolByLogic();

            // Obtener un objeto del pool seleccionado
            GameObject obj = selectedPool.GetPooledObject();

            // Verificar si el objeto es nulo (si el pool está vacío)
            if (obj == null)
            {
                Debug.LogWarning("No se pudo obtener un objeto del pool.");
            }
            else
            {
                // Activar el objeto en caso de que esté desactivado
                obj.SetActive(true);

                // Posicionar el objeto en el lugar del spawner
                obj.transform.position = transform.position;

                // También puedes resetear la rotación si es necesario
                obj.transform.rotation = Quaternion.identity;
            }

            // Esperar un tiempo aleatorio antes de spawnear otro objeto
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    ObjectPool GetRandomPool()
    {
        // Elegir un pool al azar de la lista
        int randomIndex = Random.Range(0, pools.Count);
        return pools[randomIndex];
    }

    ObjectPool SelectPoolByLogic()
    {
        // Aquí puedes agregar tu propia lógica para seleccionar un pool en lugar de usar uno aleatorio
        // Ejemplo: Basado en niveles, dificultad o el estado del juego.
        return pools[0];  // Por ejemplo, siempre devuelve el primer pool (ajustar según necesidad)
    }
}
