using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplePoolSpawner : Spawner
{
public ObjectPoolMultiple objectPoolMultiple; // Referencia al ObjectPoolMultiple
    public float minSpawnInterval = 1f;  // Intervalo mínimo entre spawns
    public float maxSpawnInterval = 3f;  // Intervalo máximo entre spawns
    public bool randomizePools = true;   // Si se debe elegir el pool de forma aleatoria

    private bool powerUpActive = false; // Para rastrear si hay un power-up activo
    public float spawnDelay = 5f; // Tiempo que debe pasar antes de iniciar la generación
    bool isOnPlay;

    void Start()
    {
        base.Start(); // Llamar a Start del spawner base
        StartCoroutine(SpawnObjects());

        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }
    void OnGameStateChanged(Game_State _gameState)
    {
        isOnPlay = _gameState == Game_State.Play;

    }
    private void Update()
    {
        if (!isOnPlay) return;
    }
    private IEnumerator SpawnObjects()
    {
        // Esperar el tiempo de retraso antes de comenzar a generar
        yield return new WaitForSeconds(spawnDelay);

        while (true)  // Mantener la generación
        {
            if (!powerUpActive && SpawnManager.PuedeGenerar(grupo))
            {
                // Elegir un prefab aleatorio basado en la probabilidad de aparición
                GameObject selectedPrefab = SelectRandomPrefab();

                // Obtener un objeto del pool seleccionado
                GameObject obj = objectPoolMultiple.GetObjectFromPool(selectedPrefab);

                // Verificar si el objeto es nulo (si el pool está vacío)
                if (obj != null)
                {
                    // Posicionar temporalmente el objeto en el spawner
                    obj.transform.position = transform.position; 
                    obj.SetActive(true);
                    powerUpActive = true;

                    // Añadir lógica para desactivar el power-up después de un tiempo
                    StartCoroutine(DeactivatePowerUp(obj));
                }
            }

            // Esperar un tiempo aleatorio antes de intentar generar otro objeto
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    GameObject SelectRandomPrefab()
    {
        // Asegúrate de que poolItems tenga elementos
        if (objectPoolMultiple.poolItems.Count == 0)
        {
            Debug.LogWarning("No hay elementos en poolItems.");
            return null; // Retorna null si no hay elementos
        }

        // Selecciona un prefab aleatorio de la lista
        return objectPoolMultiple.poolItems[Random.Range(0, objectPoolMultiple.poolItems.Count)].prefab;
    }

    IEnumerator DeactivatePowerUp(GameObject powerUp)
    {
        // Esperar un tiempo determinado antes de desactivar el power-up
        yield return new WaitForSeconds(5f);
        objectPoolMultiple.ReturnObjectToPool(powerUp);
        powerUpActive = false; // Marcar que ya no hay un power-up activo
    }
}
