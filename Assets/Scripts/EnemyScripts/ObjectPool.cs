using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 4;

    private List<GameObject> pool;

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetPooledObject()
    {
        if (pool == null || pool.Count == 0)
        {
            InitializePool();
        }
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab, transform);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
}
