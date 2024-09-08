using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    [SerializeField] private int damage;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<CombateJugador>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
