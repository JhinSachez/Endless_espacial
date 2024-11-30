using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerPersonaje : MonoBehaviour
{
    public GameObject rojoPersonaje;
    public GameObject fantasmPersonaje;
    public GameObject whitePersonaje;
    
    public bool rojo;
    public bool fantasm;
    public bool white;

    private void Update()
    {
        rojo = PlayerPrefs.GetInt("rojoSelect") == 1;
        fantasm = PlayerPrefs.GetInt("ftsmSelect") == 1;
        white = PlayerPrefs.GetInt("whiteSelect") == 1;

        if (rojo == true)
        {
            rojoPersonaje.SetActive(true);
            fantasmPersonaje.SetActive(false);
            whitePersonaje.SetActive(false);
        }
        if (fantasm == true)
        {
            rojoPersonaje.SetActive(false);
            fantasmPersonaje.SetActive(true);
            whitePersonaje.SetActive(false);
        }
        if (white == true)
        {
            rojoPersonaje.SetActive(false);
            fantasmPersonaje.SetActive(false);
            whitePersonaje.SetActive(true);
        }
    }
}
