using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool = new();

    private int _createdObjectsCount = 0;

    public int CreatedObjectsCount => _createdObjectsCount;

    public void Create(T @object)
    {
        T obj = GameObject.Instantiate(@object);

        obj.gameObject.SetActive(false);
        _pool.Add(obj);
        _createdObjectsCount++;
    }

    public T GetObj()
    {
        if (_pool.Count > 0)
        {
            foreach(var obj in _pool)
            {
                if(obj.gameObject.activeSelf == false)
                {
                    return obj;
                }
            }
        }

        return null;
    }
}
