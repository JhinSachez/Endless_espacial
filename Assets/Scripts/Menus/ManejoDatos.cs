using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejoDatos : MonoBehaviour
{
    public void BorrarDatos()
    {
        PlayerPrefs.DeleteAll();
    }
}
