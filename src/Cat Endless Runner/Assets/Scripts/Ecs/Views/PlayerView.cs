using System;
using Ecs.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Views
{
    public class PlayerView : MonoBehaviour
    {
        private int _entity;
        private EcsWorld _world;

        public void Init(int entity, EcsWorld world)
        {
            _entity = entity;
            _world = world;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<EntityView>(out var entityView))
            {
                var entity = _world.NewEntity();
                var pool = _world.GetPool<PlayerEnemyCollisionEvent>();
                ref var evt = ref pool.Add(entity);
                evt.EntityView = entityView;
                evt.PlayerEntity = _entity;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CoinView>(out var coinView))
            {
                var entity = _world.NewEntity();
                var pool = _world.GetPool<PlayerCoinCollisionEvent>();
                ref var evt = ref pool.Add(entity);
                evt.CoinView = coinView;
                evt.PlayerEntity = _entity;
            }
            else if (other.gameObject.TryGetComponent<BoostView>(out var boostView))
            {
                var entity = _world.NewEntity();
                var pool = _world.GetPool<PlayerBoostCollisionEvent>();
                ref var evt = ref pool.Add(entity);
                evt.BoostView = boostView;
                evt.PlayerEntity = _entity;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<GroundTileView>(out var groundTileView))
            {
                var entity = _world.NewEntity();
                var pool = _world.GetPool<PlayerTileCollisionExitEvent>();
                ref var evt = ref pool.Add(entity);
                evt.GroundTileView = groundTileView;
                evt.PlayerEntity = _entity;
            } 
        }
    }
}