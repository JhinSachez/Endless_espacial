using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplePoolSpawner : MonoBehaviour
{
private static float sharedCooldown = 0f; // Cooldown global para sincronizar spawners del mismo grupo
    private bool isOnPlay = false;
    private bool powerUpActive = false;

    public ObjectPoolMultiple objectPoolMultiple;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float spawnDelay = 2f;
    public float cooldownTime = 2f; // Tiempo mínimo entre generaciones de distintos spawners
    private Coroutine spawnCoroutine;

    void Start()
    {
        if (objectPoolMultiple == null)
        {
            Debug.LogError("ObjectPoolMultiple no asignado en el spawner.");
            return;
        }

        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnEnable()
    {
        if (isOnPlay && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObjects());
        }
    }

    void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;

        if (isOnPlay)
        {
            if (spawnCoroutine == null)
                spawnCoroutine = StartCoroutine(SpawnObjects());
        }
        else
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (isOnPlay)
        {
            // Esperar a que el cooldown global termine
            while (sharedCooldown > 0f)
            {
                yield return null;
            }

            sharedCooldown = cooldownTime; // Reinicia el cooldown global

            GameObject spawnedObject = TrySpawnObject();

            if (spawnedObject == null)
            {
                Debug.LogWarning($"Spawner {gameObject.name} no pudo generar un objeto. Verifica el estado del pool.");
            }

            // Esperar un tiempo aleatorio entre spawn individuales
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    GameObject TrySpawnObject()
    {
        GameObject selectedPrefab = SelectRandomPrefab();

        if (selectedPrefab == null)
        {
            Debug.LogWarning("No se pudo seleccionar un prefab. Verifica la configuración del pool.");
            return null;
        }

        GameObject obj = objectPoolMultiple.GetObjectFromPool(selectedPrefab);

        if (obj == null)
        {
            Debug.LogWarning("No se pudo obtener un objeto del pool.");
            return null;
        }

        obj.transform.position = transform.position;

        obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        Debug.Log($"Objeto generado correctamente: {obj.name} en spawner: {gameObject.name}");
        return obj;
    }

    GameObject SelectRandomPrefab()
    {
        if (objectPoolMultiple.poolItems == null || objectPoolMultiple.poolItems.Count == 0)
        {
            Debug.LogError("El pool no tiene prefabs configurados.");
            return null;
        }

        int randomIndex = Random.Range(0, objectPoolMultiple.poolItems.Count);
        return objectPoolMultiple.poolItems[randomIndex].prefab;
    }

    void Update()
    {
        // Reducir el cooldown global con el tiempo
        if (sharedCooldown > 0f)
        {
            sharedCooldown -= Time.deltaTime;
        }
    }
}
