using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrb : MonoBehaviour
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
        if ((Input.GetKey("a")))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-2, 0, 3);
            StartCoroutine(StopLineChange());
        }
        
        if ((Input.GetKey("d")))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 3);
            StartCoroutine(StopLineChange());
        }

        /*if (_distanceScore.distance => 60)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 10);
            
            if ((Input.GetKey("a")))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(-2, 0, 10);
                StartCoroutine(StopLineChange2());
            }
        
            if ((Input.GetKey("d")))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 10);
                StartCoroutine(StopLineChange2());
            }
        }*/
    }

    IEnumerator StopLineChange()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3);
    }
    
    IEnumerator StopLineChange2()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 10);
    }
}
