using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> _pool;

    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (autoExpand)
            return CreateObject(true);

        throw new Exception($"There is no free elements in pool of type{typeof(T)}");
    }

    public List<T> GetAllFreeElements()
    {
        List<T> freeElements = new List<T>();

        foreach (var element in _pool)
        {
            if (!element.gameObject.activeInHierarchy)
            {
                freeElements.Add(element);
            }
        }
        return freeElements;
    }
}
