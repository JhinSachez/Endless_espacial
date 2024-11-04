using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool isOnPlay;
    public ObjectPool objectPool; // Referencia al Object Pool
    
    public string grupo = "GrupoSimple";

    public float tiempoMinimoEntreSpawns = 1f; // Tiempo mínimo entre spawns
    public float tiempoMaximoEntreSpawns = 5f; // Tiempo máximo entre spawns

    public float tiempoExtra = 2f;
    public float tiempoInactividad = 3f;
    
    private float tiempoSiguienteSpawn;  
    
    private bool generando = false;


    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        
        tiempoSiguienteSpawn = Time.time + Random.Range(tiempoMinimoEntreSpawns, tiempoMaximoEntreSpawns);

    }

    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }

    private void Update()
    {
        if (!isOnPlay) return;
        
        if (!generando && Time.time >= tiempoSiguienteSpawn && SpawnManager.PuedeGenerar(grupo, true))
        {
            // Comienza la generación
            StartCoroutine(GenerarObjeto());
        }
    }

    private IEnumerator GenerarObjeto()
    {
        generando = true; // Indica que está generando
        SpawnManager.ComienzaGenerar(grupo);
        SpawnManager.SetSpawnerSimpleActivo(true);// Bloquea el otro spawner

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
            tiempoSiguienteSpawn = Time.time + Random.Range(tiempoMinimoEntreSpawns, tiempoMaximoEntreSpawns);

            // Aquí podrías añadir una pequeña espera si es necesario
            yield return null;
        }
        finally
        {
            // Asegura que el grupo se libere incluso si hay un error
            SpawnManager.TerminaGenerar(grupo);
            SpawnManager.SetSpawnerSimpleActivo(false);
            SpawnManager.EstablecerTiempoInactividad(tiempoInactividad);
            generando = false;
        }
    }
}
