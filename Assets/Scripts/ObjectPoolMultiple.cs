using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMultiple : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab; // Prefab específico
        public int poolSize; // Tamaño del pool para este objeto
    }

    public List<PoolItem> poolItems; // Lista de diferentes objetos (prefabs)
    private Dictionary<GameObject, List<GameObject>> poolDictionary; // Almacena los pools para cada prefab

    void Start()
    {
        poolDictionary = new Dictionary<GameObject, List<GameObject>>();

        // Crear un pool para cada tipo de prefab en poolItems
        foreach (PoolItem item in poolItems)
        {
            List<GameObject> objectPool = new List<GameObject>();

            // Crear las instancias iniciales y desactivarlas
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(item.prefab, objectPool);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (poolDictionary.ContainsKey(prefab))
        {
            List<GameObject> objectPool = poolDictionary[prefab];

            // Buscar un objeto desactivado
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // Si no hay objetos disponibles, instanciar uno nuevo y añadirlo al pool (opcional)
            GameObject newObj = Instantiate(prefab);
            objectPool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogWarning("El prefab no está en el pool.");
            return null;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
