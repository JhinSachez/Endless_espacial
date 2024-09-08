using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombateJugador : MonoBehaviour
{

    [SerializeField] public int Vida;

    public event EventHandler MuerteJugador;

    public void TakeDamage(int amountDamage)
    {
        Vida -= amountDamage;
        if (Vida <= 0) 
        {
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

}
 