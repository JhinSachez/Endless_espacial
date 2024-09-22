using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerShleld._isShieldOn = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * 5f * Time.deltaTime);
    }
}
