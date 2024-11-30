using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPersonaje : MonoBehaviour
{
    public bool rojo;
    public bool fantasm;
    public bool white;
    
    public void cometaRojo()
    {
        rojo = true;
        fantasm = false;
        white = false;
        guardar();
    }

    public void fantasma()
    {
        rojo = false;
        fantasm = true;
        white = false;
        guardar();
    }

    public void meteoroBlanco()
    {
        rojo = false;
        fantasm = false;
        white = true;
        guardar();
    }

    public void guardar()
    {
        PlayerPrefs.SetInt("rojoSelect", rojo ? 1:0);
        PlayerPrefs.SetInt("ftsmSelect", fantasm ? 1:0);
        PlayerPrefs.SetInt("whiteSelect", white ? 1:0);
    }
}
