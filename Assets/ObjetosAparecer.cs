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
    public Camera cam;

    // Lista de spawners para activar/desactivar según el estado del juego
    public List<MultiplePoolSpawner> spawners;

    private void Start()
    {
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        if (_distanceScore.distance >= 10000)
        {
            IsOnEarth = false;
            IsOnSky = true;
            Transicion.SetActive(true);

            StartCoroutine(Disappear());
        }
        
        if(_distanceScore.distance >= 20000)
        {
            IsOnSky = false;
            IsOnSpace = true;
            Transicion.SetActive(true);

            StartCoroutine(Disappear2());
        }
    }

    IEnumerator Disappear()
    {
        
        yield return new WaitForSecondsRealtime(5);
        Sky.SetActive(true);
    }
    
    IEnumerator Disappear2()
    {
        
        yield return new WaitForSecondsRealtime(5);
        cam.backgroundColor = Color.black;
        Space.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Tierra");
        GameObject[] gameObjectArray2 = GameObject.FindGameObjectsWithTag("Cielo");

        foreach (GameObject go in gameObjectArray)
        {
            go.SetActive(false);
        }
        
        foreach (GameObject go in gameObjectArray2)
        {
            go.SetActive(false);
        }
    }
}
