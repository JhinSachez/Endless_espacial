using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPool objectPool; // Referencia al Object Pool
    public string grupo = "GrupoSimple";

    public float tiempoMinimoEntreSpawns = 1f; // Tiempo mínimo entre spawns
    public float tiempoMaximoEntreSpawns = 5f; // Tiempo máximo entre spawns

    private float tiempoSiguienteSpawn;  
    private bool generando = false;
    public float spawnDelay = 5f;
    bool isOnPlay;

    public void Start()
    {
        SpawnManager.RegisterSpawner(this); // Registrar el spawner en el SpawnManager
        tiempoSiguienteSpawn = Time.time + Random.Range(tiempoMinimoEntreSpawns, tiempoMaximoEntreSpawns);

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
    void OnDestroy()
    {
        SpawnManager.UnregisterSpawner(this); // Desregistrar el spawner
    }

    public bool CanGenerate()
    {
        return !generando && Time.time >= tiempoSiguienteSpawn && SpawnManager.PuedeGenerar(grupo);
    }

    public void StartGenerating()
    {
        if (!generando) // Verifica que no se esté generando ya
        {
            StartCoroutine(GenerarObjeto());
        }
    }

    private IEnumerator GenerarObjeto()
    {
        generando = true; // Indica que está generando
        SpawnManager.ComienzaGenerar(grupo);

        try
        {
            // Genera el objeto del pool
            GameObject objeto = objectPool.GetPooledObject();
            if (objeto != null)
            {
                objeto.transform.position = transform.position;
                objeto.SetActive(true);
            }

            // Calcula el tiempo para el siguiente spawn
            tiempoSiguienteSpawn = Time.time + Random.Range(tiempoMinimoEntreSpawns, tiempoMaximoEntreSpawns + spawnDelay);

            yield return null;
        }
        finally
        {
            SpawnManager.TerminaGenerar(grupo);
            generando = false;
        }
    }
}
