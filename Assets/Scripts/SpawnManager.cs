using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static Dictionary<string, bool> grupoActivo = new Dictionary<string, bool>();
    private static Dictionary<string, bool> grupoMultipleActivo = new Dictionary<string, bool>();
    private static List<Spawner> spawners = new List<Spawner>(); // Lista de spawners
    private static float tiempoInactividad = 0f;

    public static void RegisterSpawner(Spawner spawner)
    {
        if (!spawners.Contains(spawner))
        {
            spawners.Add(spawner);
        }
    }

    public static void UnregisterSpawner(Spawner spawner)
    {
        if (spawners.Contains(spawner))
        {
            spawners.Remove(spawner);
        }
    }

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

    public static bool PuedeGenerar(string grupo)
    {
        return !grupoActivo.ContainsKey(grupo) || !grupoActivo[grupo];
    }

    private void Update()
    {
        // Se activa la generación de objetos en los spawners
        foreach (var spawner in spawners)
        {
            if (spawner.CanGenerate())
            {
                spawner.StartGenerating(); // Llama al método que activa la generación
            }
        }

        // Maneja el tiempo de inactividad
        if (tiempoInactividad > 0)
        {
            tiempoInactividad -= Time.deltaTime;
        }
    }
}
