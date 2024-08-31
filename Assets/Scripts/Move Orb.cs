using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrb : MonoBehaviour
{

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
    }

    IEnumerator StopLineChange()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3);
    }
}
