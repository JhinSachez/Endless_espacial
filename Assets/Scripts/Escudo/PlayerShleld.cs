using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShleld : MonoBehaviour
{

    public static bool _isShieldOn;
    public GameObject escudo;


    void Start()
    {
        _isShieldOn = false;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isShieldOn && other.CompareTag("Enemigo"))
        {
            //escudo.SetActive(false);
            _isShieldOn = false;
            Debug.Log(_isShieldOn);
        }

        
    }

    void Update()
    {
        if (_isShieldOn)
        {
            escudo.SetActive(true);
        }
        else if (_isShieldOn == false)
        {
            escudo.SetActive(false);
        }
    }
}
