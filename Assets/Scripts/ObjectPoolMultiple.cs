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

    void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        poolDictionary = new Dictionary<GameObject, List<GameObject>>();

        foreach (PoolItem item in poolItems)
        {
            if (item.prefab == null)
            {
                Debug.LogError("Prefab no asignado en un PoolItem. Verifica las configuraciones.");
                continue;
            }

            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(item.prefab, objectPool);
        }

        Debug.Log($"Pool inicializado con {poolDictionary.Count} tipos de objetos.");
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (poolDictionary == null)
        {
            Debug.LogError("El pool no está inicializado. Llama a InitializePool antes de usarlo.");
            return null;
        }

        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError($"El prefab {prefab.name} no está registrado en el pool.");
            return null;
        }

        List<GameObject> objectPool = poolDictionary[prefab];

        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                Debug.Log($"Objeto obtenido del pool: {obj.name}");
                return obj;
            }
        }

        Debug.LogWarning($"No hay objetos disponibles en el pool para {prefab.name}. Instanciando uno nuevo.");
        GameObject newObj = Instantiate(prefab);
        objectPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
