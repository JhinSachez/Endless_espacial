using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetosAparecer : MonoBehaviour
{
    public DistanceScore _distanceScore;
    public bool IsOnEarth = true;
    public bool IsOnSky;
    public bool IsOnSpace;
    public GameObject Earth;
    public GameObject Sky;
    public GameObject Space;
    public GameObject Transicion;

    // Lista de spawners para activar/desactivar según el estado del juego
    public List<MultiplePoolSpawner> spawners;

    void Update()
    {
        if (_distanceScore.distance >= 10000)
        {
            IsOnEarth = false;
            IsOnSky = true;
            Transicion.SetActive(true);

            UpdateSpawnerBools();
            StartCoroutine(Disappear());
        }
    }

    private void UpdateSpawnerBools()
    {
        foreach (var spawner in spawners)
        {
            spawner.puedeGenerarEnTierra = IsOnEarth;
            spawner.puedeGenerarEnCielo = IsOnSky;
            spawner.puedeGenerarEnEspacio = IsOnSpace;
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSecondsRealtime(5);
        Sky.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Tierra");

        foreach (GameObject go in gameObjectArray)
        {
            go.SetActive(false);
        }
    }
}
