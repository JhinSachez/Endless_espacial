using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplePoolSpawner : MonoBehaviour
{
   private static float sharedCooldown = 0f; // Tiempo de inactividad compartido entre todos los spawners
    private static bool isSpawningActive = false; // Bandera para saber si se puede generar

    bool isOnPlay;
    public ObjectPoolMultiple objectPoolMultiple; // Referencia al ObjectPoolMultiple
    public float minSpawnInterval = 1f;  // Intervalo mínimo entre spawns
    public float maxSpawnInterval = 3f;  // Intervalo máximo entre spawns
    public bool randomizePools = true;   // Si se debe elegir el pool de forma aleatoria

    private bool powerUpActive = false; // Para rastrear si hay un power-up activo

    public float spawnDelay = 5f; // Tiempo que debe pasar antes de iniciar la generación
    public float cooldownTime = 5f; // Tiempo de inactividad después de generar un Power-Up

    void Start()
    {
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

IEnumerator SpawnObjects()
{
    // Esperar el tiempo de retraso antes de comenzar a generar
    yield return new WaitForSeconds(spawnDelay);

    while (isOnPlay)  // Solo generar objetos si el juego está en estado "Play"
    {
        // Comprobar si se puede generar un Power-Up
        if (!powerUpActive && sharedCooldown <= 0f)
        {
            // Elegir un prefab aleatorio basado en la probabilidad de aparición
            GameObject selectedPrefab = SelectRandomPrefab();

            // Obtener un objeto del pool seleccionado
            GameObject obj = objectPoolMultiple.GetObjectFromPool(selectedPrefab);

            // Verificar si el objeto es nulo (si el pool está vacío)
            if (obj == null)
            {
                Debug.LogWarning("No se pudo obtener un objeto del pool.");
            }
            else
            {
                // Posicionar temporalmente el objeto en el spawner
                obj.transform.position = transform.position; 
                obj.transform.rotation = Quaternion.identity;

                // Comprobar si la posición está libre usando un "Overlap" para verificar colisiones
                Collider[] colliders = Physics.OverlapSphere(obj.transform.position, 1f, LayerMask.GetMask("PowerUp", "Obstacle")); // Ajusta el radio según el tamaño del objeto

                if (colliders.Length == 0)
                {
                    // Si no hay colisión, activar el objeto y marcarlo como activo
                    obj.SetActive(true);
                    powerUpActive = true;
                    sharedCooldown = cooldownTime;

                    Debug.Log("Power-Up activado: " + obj.name);

                    // Añadir lógica para desactivar el power-up después de un tiempo
                    StartCoroutine(DeactivatePowerUp(obj));
                }
                else
                {
                    // Si hay colisión, devolver el objeto al pool sin activarlo
                    Debug.LogWarning("Posición ocupada, no se puede generar Power-Up aquí.");
                    objectPoolMultiple.ReturnObjectToPool(obj);
                }
            }
        }
        else
        {
            Debug.Log("No se puede generar un nuevo Power-Up, uno ya está activo o cooldown activo.");
        }

        // Esperar un tiempo aleatorio antes de intentar generar otro objeto
        float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(waitTime);
    }
}
    GameObject SelectRandomPrefab()
    {
        // Aquí puedes implementar la lógica de probabilidad
        float randomValue = Random.value;

        if (randomValue < 0.5f) // 50% de probabilidad
        {
            return objectPoolMultiple.poolItems[0].prefab; // Primer prefab
        }
        else if (randomValue < 0.8f) // 30% de probabilidad
        {
            return objectPoolMultiple.poolItems[1].prefab; // Segundo prefab
        }
        else // 20% de probabilidad
        {
            return objectPoolMultiple.poolItems[2].prefab; // Tercer prefab
        }
    }

    IEnumerator DeactivatePowerUp(GameObject powerUp)
    {
        // Esperar un tiempo determinado antes de desactivar el power-up
        yield return new WaitForSeconds(5f); // Ajusta este tiempo según necesites
        objectPoolMultiple.ReturnObjectToPool(powerUp);
        powerUpActive = false; // Marcar que ya no hay un power-up activo

        Debug.Log("Power-Up desactivado: " + powerUp.name);
    }

    private void Update()
    {
        // Reducir el tiempo de inactividad compartido si es mayor que 0
        if (sharedCooldown > 0)
        {
            sharedCooldown -= Time.deltaTime;
        }
    }
}
