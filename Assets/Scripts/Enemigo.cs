using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    [SerializeField] private int da�o;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            collider2D.GetComponent<CombateJugador>().TomarDa�o(da�o);
            Destroy(gameObject);
        }

    }
    
}
