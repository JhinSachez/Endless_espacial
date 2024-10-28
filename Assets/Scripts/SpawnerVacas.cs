using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerVacas : Spawner
{
    public string grupo = "GrupoSerie";

    public float tiempoMinimoEntreSeries = 3f; // Tiempo mínimo de espera entre series
    public float tiempoMaximoEntreSeries = 6f; // Tiempo máximo de espera entre series

    public int numeroMinimoObjetosPorSerie = 1; // Mínimo número de objetos por serie
    public int numeroMaximoObjetosPorSerie = 5; // Máximo número de objetos por serie
    public float tiempoEntreObjetosConsecutivos = 0.5f; // Tiempo entre objetos consecutivos en la misma serie

    private float tiempoSiguienteSerie; // Tiempo de inicio para la próxima serie
    private bool generandoSerie = false;

    void Start()
    {
        base.Start(); // Llamar a Start del spawner base
        tiempoSiguienteSerie = Time.time + Random.Range(tiempoMinimoEntreSeries, tiempoMaximoEntreSeries);
    }

    public new void StartGenerating()
    {
        if (!generandoSerie && Time.time >= tiempoSiguienteSerie && SpawnManager.PuedeGenerar(grupo))
        {
            StartCoroutine(GenerarSerieDeObjetos());
        }
    }

    private IEnumerator GenerarSerieDeObjetos()
    {
        generandoSerie = true; // Indica que estamos generando una serie
        SpawnManager.ComienzaGenerar(grupo); // Bloquea el grupo

        try
        {
            int objetosEnEstaSerie = Random.Range(numeroMinimoObjetosPorSerie, numeroMaximoObjetosPorSerie + 1);

            for (int i = 0; i < objetosEnEstaSerie; i++)
            {
                GameObject objeto = objectPool.GetPooledObject();
                objeto.transform.position = transform.position;
                objeto.SetActive(true);

                yield return new WaitForSeconds(tiempoEntreObjetosConsecutivos);
            }
        }
        finally
        {
            tiempoSiguienteSerie = Time.time + Random.Range(tiempoMinimoEntreSeries, tiempoMaximoEntreSeries);
            SpawnManager.TerminaGenerar(grupo); // Libera el grupo
            generandoSerie = false;
        }
    }
}
