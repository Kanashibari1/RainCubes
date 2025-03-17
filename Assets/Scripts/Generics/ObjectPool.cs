using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private const int _capacity = 5;

    private List<T> _pool = new();

    public int CreatedObjectsCount { get; private set; }

    public ObjectPool(T _prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Create(_prefab);
        }
    }

    private T Create(T @object)
    {
        T obj = GameObject.Instantiate(@object);

        obj.gameObject.SetActive(false);
        _pool.Add(obj);
        CreatedObjectsCount++;
        return obj;
    }

    public T GetObj(T prototype)
    {
        if (_pool.Count > 0)
        {
            foreach (var @object in _pool)
            {
                if (@object.gameObject.activeSelf == false)
                {
                    return @object;
                }
            }
        }

        return Create(prototype);
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}
