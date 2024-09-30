using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static Dictionary<string, bool> grupoActivo = new Dictionary<string, bool>();
    private static Dictionary<string, bool> grupoMultipleActivo = new Dictionary<string, bool>(); // Para grupos múltiples
    private static bool spawnerSimpleActivo = false;
    private static float tiempoInactividad = 0f; // Tiempo de inactividad para spawners simples

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
            // Permitir generación en spawners simples solo si ninguno está activo y no hay inactividad
            return !spawnerSimpleActivo && (!grupoActivo.ContainsKey(grupo) || !grupoActivo[grupo]) && tiempoInactividad <= 0;
        }
        else if (esGrupoMultiple)
        {
            // Permitir generación en grupos múltiples si el grupo no está activo
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
        // Reducir el tiempo de inactividad si es mayor que 0
        if (tiempoInactividad > 0)
        {
            tiempoInactividad -= Time.deltaTime;
        }
    }
}
