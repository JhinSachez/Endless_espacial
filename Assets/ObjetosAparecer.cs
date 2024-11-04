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
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        if (_distanceScore.distance >= 10000)
        {
            IsOnEarth=false;
            IsOnSky=true;
            Transicion.SetActive(true);
            StartCoroutine(Disappear());
        }
            
        
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSecondsRealtime(6);
        Sky.SetActive(true);
    }
    

    
    private void OnTriggerEnter(Collider other)
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Tierra");


        foreach(GameObject go in gameObjectArray)
        {
            go.SetActive(false);
        }

        
    }
}
