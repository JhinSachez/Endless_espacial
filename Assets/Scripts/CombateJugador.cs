using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombateJugador : MonoBehaviour
{

    [SerializeField] public int Vida;

    //public event EventHandler MuerteJugador;

    public bool muerteJugador = false;


    public void TakeDamage(int amountDamage)
    {
        Vida -= amountDamage;
        if (Vida <= 0) 
        {
            muerteJugador = true;
            GameManager.GetInstance().ChangeGameState(Game_State.Game_Over);
        }
    }

}
 