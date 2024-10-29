using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(_distanceScore.distance >= 100)
        {
            IsOnEarth=false;
            IsOnSky=true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSecondsRealtime(6);
        Earth.SetActive(false);
        Sky.SetActive(true);

    }
}
