using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BasePool : MonoBehaviour
{
    
    [SerializeField] private GameObject _object;
    [SerializeField] private int _count;
    [SerializeField, ReadOnly] private List<GameObject> _pool = new List<GameObject>();

    [Inject]
    DiContainer _container;
    

    private void InstantiatePool()
    {
        for (int i = 0; i < _count; i++)
        {
            AddObjectToPool(_object);
        }
    }

    private void AddObjectToPool(GameObject prefab)
    {
        GameObject tmp = _container.InstantiatePrefab(prefab);
        tmp.SetActive(false);
        _pool.Add(tmp);
    }
    
    protected void Awake()
    {
        InstantiatePool();
    }


    public GameObject GetRandomObject()
    {
        List<GameObject> unActive = _pool.FindAll(objectFromPool => !objectFromPool.activeInHierarchy);
        if (unActive.Count <= 0)
        {
            AddObjectToPool(_object);
            return _pool[^1];
        }

        int randomIndex = Random.Range(0, unActive.Count - 1);

        return unActive[randomIndex];
    }
}