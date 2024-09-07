using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombateJugador : MonoBehaviour
{

    [SerializeField] private int Vida;

    public event EventHandler MuerteJugador;

    public void TomarDaño(int CantidadDaño)
    {
        Vida -= CantidadDaño;
        if (Vida <= 0) 
        {
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

}
 