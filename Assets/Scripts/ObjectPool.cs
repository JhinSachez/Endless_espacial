using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
// El objeto que vamos a instanciar y reutilizar
    public GameObject prefab;

    // Lista que contiene los objetos disponibles en el pool
    private List<GameObject> pool = new List<GameObject>();

    // Número inicial de objetos en el pool
    public int poolSize = 10;

    private void Start()
    {
        // Inicializa el pool instanciando una cantidad inicial de objetos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // Desactiva el objeto
            pool.Add(obj); // Añádelo al pool
        }
    }

    // Método para obtener un objeto del pool
    public GameObject GetPooledObject()
    {
        // Busca un objeto desactivado en el pool
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj; // Devuelve el primer objeto inactivo que encuentre
            }
        }

        // Si no hay objetos inactivos, se puede crear uno nuevo si es necesario
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false); // Desactívalo
        pool.Add(newObj); // Agrégalo al pool
        return newObj;
    }
}
