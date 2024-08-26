using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Ecs.Services
{
    public class Pool<T> where T : Component
    {
        private readonly List<PoolNode<T>> _items;
        private readonly Transform _parent;
        private readonly T _prefab;
        
        private Action<T> _onItemCreatedAction;

        // todo оптимизировать это
        public List<T> ActiveItems => _items.Where(x => x.IsActive).Select(x => x.Value).ToList();
        
        public Pool(T prefab, Transform parent = null, Action<T> onItemCreatedAction = null)
        {
            Assert.IsNotNull(prefab);
            
            _parent = parent;
            _prefab = prefab;
            _onItemCreatedAction = onItemCreatedAction;
            _items = new List<PoolNode<T>>();
        }

        public void SetItemCreatedAction(Action<T> onItemCreatedAction)
        {
            _onItemCreatedAction = onItemCreatedAction;
        }
        
        public T Get()
        {
            var poolNode = _items.FirstOrDefault(x => !x.IsActive);
            if (poolNode == null)
            {
                poolNode = Create();
            }
            
            poolNode.Value.gameObject.SetActive(true);
            poolNode.IsActive = true;
            return poolNode.Value;
        }

        public void ReturnInPool(T item)
        {
            foreach (var poolNode in _items)
            {
                if (ReferenceEquals(poolNode.Value, item))
                {
                    poolNode.IsActive = false;
                    poolNode.Value.gameObject.SetActive(false);
                    return;
                }
            }
            
            Debug.LogError($"Item not found in pool.");
        }

        private PoolNode<T> Create()
        {
            if (_items.Count > 1000)
            {
                Debug.LogError("Count item is too much.");
            }

            var item = Object.Instantiate(_prefab, _parent);
            var poolNode = new PoolNode<T>(item, false);
            _items.Add(poolNode);
            _onItemCreatedAction?.Invoke(item);
            return poolNode;
        }
    }
}