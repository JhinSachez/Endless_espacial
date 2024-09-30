using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

     public int damage;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && PlayerShleld._isShieldOn == false)
        {
            collider.GetComponent<CombateJugador>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
