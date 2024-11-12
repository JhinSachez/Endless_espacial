using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplePoolSpawner : MonoBehaviour
{
    private static float sharedCooldown = 0f;
    bool isOnPlay;
    public ObjectPoolMultiple objectPoolMultiple;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    private bool powerUpActive = false;
    public float spawnDelay = 5f;
    public float cooldownTime = 5f;

    // Bools de condición para generar
    public bool puedeGenerarEnTierra;
    public bool puedeGenerarEnCielo;
    public bool puedeGenerarEnEspacio;
    
    private Coroutine spawnCoroutine;

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;

        if (isOnPlay)
        {
            if (spawnCoroutine != null)
                StopCoroutine(spawnCoroutine);

            // Reiniciar el cooldown y empezar el spawn cuando el juego esté en Play
            sharedCooldown = 0f;
            spawnCoroutine = StartCoroutine(SpawnObjects());
        }
        else
        {
            if (spawnCoroutine != null)
                StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (isOnPlay)
        {
            if (PuedeGenerar() && !powerUpActive && sharedCooldown <= 0f)
            {
                GameObject selectedPrefab = SelectRandomPrefab();
                GameObject obj = objectPoolMultiple.GetObjectFromPool(selectedPrefab);

                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = Quaternion.identity;

                    Collider[] colliders = Physics.OverlapSphere(obj.transform.position, 1f, LayerMask.GetMask("PowerUp", "Obstacle"));
                    if (colliders.Length == 0)
                    {
                        obj.SetActive(true);
                        powerUpActive = true;
                        sharedCooldown = cooldownTime;

                        Debug.Log("Power-Up activado: " + obj.name);
                        StartCoroutine(DeactivatePowerUp(obj));
                    }
                    else
                    {
                        objectPoolMultiple.ReturnObjectToPool(obj);
                    }
                }
            }
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private bool PuedeGenerar()
    {
        if (puedeGenerarEnTierra && this.CompareTag("Tierra")) return true;
        if (puedeGenerarEnCielo && this.CompareTag("Cielo")) return true;
        if (puedeGenerarEnEspacio && this.CompareTag("Espacio")) return true;
        if (this.CompareTag("powerUp")) return true;
        return false;
    }

    GameObject SelectRandomPrefab()
    {
        // Asegurarse de que hay elementos en el pool antes de intentar seleccionar uno
        if (objectPoolMultiple.poolItems == null || objectPoolMultiple.poolItems.Count == 0)
        {
            Debug.LogWarning("No hay elementos en el pool para seleccionar.");
            return null;
        }

        // Seleccionar un índice aleatorio dentro del rango de la lista de prefabs
        int randomIndex = Random.Range(0, objectPoolMultiple.poolItems.Count);
        return objectPoolMultiple.poolItems[randomIndex].prefab;
    }

    private IEnumerator DeactivatePowerUp(GameObject powerUp)
    {
        yield return new WaitForSeconds(5f);
        objectPoolMultiple.ReturnObjectToPool(powerUp);
        powerUpActive = false;
    }

    private void Update()
    {
        if (sharedCooldown > 0)
            sharedCooldown -= Time.deltaTime;
    }
}
