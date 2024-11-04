using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static Dictionary<string, bool> grupoActivo = new Dictionary<string, bool>();
    private static Dictionary<string, bool> grupoMultipleActivo = new Dictionary<string, bool>();
    private static bool spawnerSimpleActivo = false;
    private static float tiempoInactividad = 0f;

    public static void ComienzaGenerar(string grupo, bool esGrupoMultiple = false)
    {
        if (esGrupoMultiple)
        {
            grupoMultipleActivo[grupo] = true;
        }
        else
        {
            grupoActivo[grupo] = true;
        }
    }

    public static void TerminaGenerar(string grupo, bool esGrupoMultiple = false)
    {
        if (esGrupoMultiple)
        {
            grupoMultipleActivo[grupo] = false;
        }
        else
        {
            grupoActivo[grupo] = false;
        }
    }

    public static bool PuedeGenerar(string grupo, bool esSpawnerSimple = false, bool esGrupoMultiple = false)
    {
        if (esSpawnerSimple)
        {
            return !spawnerSimpleActivo && (!grupoActivo.ContainsKey(grupo) || !grupoActivo[grupo]) && tiempoInactividad <= 0;
        }
        else if (esGrupoMultiple)
        {
            return !grupoMultipleActivo.ContainsKey(grupo) || !grupoMultipleActivo[grupo];
        }

        return !grupoActivo.ContainsKey(grupo) || !grupoActivo[grupo];
    }

    public static void SetSpawnerSimpleActivo(bool activo)
    {
        spawnerSimpleActivo = activo;
    }

    public static void EstablecerTiempoInactividad(float tiempo)
    {
        tiempoInactividad = tiempo;
    }

    private void Update()
    {
        if (tiempoInactividad > 0)
        {
            tiempoInactividad -= Time.deltaTime;
        }
    }
}
