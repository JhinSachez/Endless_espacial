using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static Dictionary<string, bool> grupoActivo = new Dictionary<string, bool>();
    private static bool spawnerSimpleActivo = false;
    private static float tiempoInactividad = 0f; // Tiempo de inactividad para spawners simples

    public static void ComienzaGenerar(string grupo)
    {
        grupoActivo[grupo] = true;
    }

    public static void TerminaGenerar(string grupo)
    {
        grupoActivo[grupo] = false;
    }

    public static bool PuedeGenerar(string grupo, bool esSpawnerSimple = false)
    {
        if (esSpawnerSimple)
        {
            // Solo permite generar si ningún spawner simple está activo y no ha pasado el tiempo de inactividad
            return !spawnerSimpleActivo && (!grupoActivo.ContainsKey(grupo) || !grupoActivo[grupo]) && tiempoInactividad <= 0;
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
