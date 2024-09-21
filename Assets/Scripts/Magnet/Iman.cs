using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Coin._magnetOn = true;
            Destroy(gameObject); 
        }
    }


    void Update()
    {
        transform.Translate(Vector3.back * 5f * Time.deltaTime);
    }
}
