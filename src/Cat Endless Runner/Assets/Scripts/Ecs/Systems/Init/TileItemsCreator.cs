using System.Collections.Generic;
using Ecs.Extensions;
using Ecs.Services;
using Ecs.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace Ecs.Systems.Init
{
    public class TileItemsCreator : IEcsInitSystem
    {
        private EcsCustomInject<GameSceneData> _sceneData;

        private Pool<Transform> _boostPool;
        private Pool<Transform> _birdPool;
        private Pool<Transform> _coinPool;
        private Pool<Transform> _dogPool;
        private List<Pool<Transform>> _flowerPools;

        public void Init(IEcsSystems systems)
        {
            _boostPool = new Pool<Transform>(_sceneData.Value.BoostPrefab.transform);
            _boostPool.SetItemCreatedAction(item => item.AddComponent<BoostView>().Pool = _boostPool);

            _coinPool = new Pool<Transform>(_sceneData.Value.CoinPrefab.transform);
            _coinPool.SetItemCreatedAction(item => item.AddComponent<CoinView>().Pool = _coinPool);

            _birdPool = new Pool<Transform>(_sceneData.Value.BirdPrefab.transform);
            _birdPool.SetItemCreatedAction(item => item.AddComponent<EntityView>().Pool = _birdPool);
            
            _dogPool = new Pool<Transform>(_sceneData.Value.DogPrefab.transform);
            _dogPool.SetItemCreatedAction(item => item.AddComponent<EntityView>().Pool = _dogPool);

            _flowerPools = new List<Pool<Transform>>();
            foreach (var flower in _sceneData.Value.FlowerPrefabs)
            {
                var flowerPool = new Pool<Transform>(flower.transform);
                flowerPool.SetItemCreatedAction(item => item.AddComponent<EntityView>().Pool = flowerPool);
                _flowerPools.Add(flowerPool);
            }
        }

        public Transform CreateBoost()
        {
            return _boostPool.Get();
        }
        
        public Transform CreateCoin()
        {
            return _coinPool.Get();
        }

        public Transform CreateEntity(EntityType entityType)
        {
            Transform entity;
            if (entityType == EntityType.Dog)
            {
                entity = _dogPool.Get();
            }
            else if (entityType == EntityType.Bird)
            {
                entity = _birdPool.Get();
            }
            else
            {
                if (_flowerPools.Count <= 0)
                {
                    Debug.LogError("error");
                    return null;
                }
                
                entity = _flowerPools.GetRandomValue().Get();
            }

            return entity;
        }

        public Transform CreateEntity()
        {
            var entityIndex = Random.Range(0, 3);
            if (entityIndex == 0)
            {
                return CreateEntity(EntityType.Bird);
            }
            
            if (entityIndex == 1)
            {
                return CreateEntity(EntityType.Dog);
            } 

            return CreateEntity(EntityType.Flower);
        }

        public void ReturnInPool(CoinView coinView)
        {
            coinView.Pool.ReturnInPool(coinView.transform);
        }
        
        public void ReturnInPool(BoostView boostView)
        {
            boostView.Pool.ReturnInPool(boostView.transform);
        }
        
        public void ReturnInPool(EntityView entityView)
        {
            entityView.Pool.ReturnInPool(entityView.transform);
        }
    }
}