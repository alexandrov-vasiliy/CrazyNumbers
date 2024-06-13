using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesPool : MonoBehaviour
{
    public static ObstaclesPool Get;
    
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private int _count;
    [SerializeField, ReadOnly] private List<GameObject> _pool = new List<GameObject>();
    [SerializeField, ReadOnly] private List<GameObject> _usingObjects = new List<GameObject>();


    private void Filling()
    {
        for (int i = 0; i < _count; i++)
        {
            _usingObjects.Add(_objects[Random.Range(0, _objects.Length)]);
        }
    }

    private void InstantiatePool()
    {
        GameObject tmp;
        for (int i = 0; i < _usingObjects.Count; i++)
        {
            if(_usingObjects[i] == null) continue;
            
            tmp = Instantiate(_usingObjects[i]);
            tmp.SetActive(false);
            _pool.Add(tmp);
        }
    }


    protected void Awake()
    {
        Get = this;
        Filling();
        InstantiatePool();
    }


    public GameObject GetRandomObject()
    {
        List<GameObject> unActive = _pool.FindAll(objectFromPool => !objectFromPool.activeInHierarchy);
        if (unActive.Count <= 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, unActive.Count - 1);

        return unActive[randomIndex];
    }
}