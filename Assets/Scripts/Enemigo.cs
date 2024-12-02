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
            AudioManager.instance.PlayGameOver();

           collider.GetComponent<PlayerCollision>().particulasColision();

            collider.GetComponent<CombateJugador>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        
        if (collider.CompareTag("Player"))
        {
            Debug.Log("choco con el enemigo");
        }


        if (collider.CompareTag("Player") && PlayerShleld._isShieldOn == true)
        {
            PlayerShleld._isShieldOn = false;

            collider.GetComponent<PlayerCollision>().particulasColision();

            gameObject.SetActive(false);
        }


        if (collider.CompareTag("Player"))
        {
            Debug.Log("choco con el enemigo");
        }


        if (collider.CompareTag("Player") && PlayerShleld._isShieldOn == true)
        {
            PlayerShleld._isShieldOn = false;

            collider.GetComponent<PlayerCollision>().particulasColision();

            gameObject.SetActive(false);
        }
    }
}
