using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public DistanceScore _distanceScore;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_distanceScore.GetComponent<DistanceScore>().distance >= 60)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 10);
        }
    }
}
