using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        private T Prefab { get; }
        public bool AutoExpand { get; set; }
        private Transform Container { get; }

        private List<T> _pool;

        public PoolMono(T prefab, int count)
        {
            Prefab = prefab;
            Container = null;

            CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            Prefab = prefab;
            Container = container;

            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (var i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(Prefab, Container);
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

            if (AutoExpand)
                return CreateObject(true);

            throw new Exception($"There is no free elements in pool of type{typeof(T)}");
        }

        public List<T> GetAllFreeElements()
        {
            return _pool.Where(element => !element.gameObject.activeInHierarchy).ToList();
        }
    }
}