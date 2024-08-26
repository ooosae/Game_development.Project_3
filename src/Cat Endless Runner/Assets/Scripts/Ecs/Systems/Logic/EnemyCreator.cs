using Ecs.Enemy;
using Ecs.Services;
using Ecs.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace Ecs.Systems.Logic
{
    public class EnemyCreator : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<Components.Enemy> _enemyPool;
        private EcsCustomInject<GameSceneData> _sceneData; 
        private EcsFilterInject<Inc<Components.Enemy>> _enemyFilter;
        
        private Pool<EnemyView> _enemies;

        public void Init(IEcsSystems systems)
        {
            _enemies = new Pool<EnemyView>(_sceneData.Value.LeoPrefab);
            _enemies.SetItemCreatedAction((enemy) =>
            {
                enemy.AddComponent<EnemyView>();
            });
        }

        public void CreateEnemy(Vector3 position, int lane)
        {
            var entity = _world.Value.NewEntity();
            ref var enemyEntity = ref _enemyPool.Value.Add(entity);
            
            var enemyView = _enemies.Get();
            enemyView.transform.position = position - new Vector3(0, 0, 5);
            enemyView.Init(entity);

            enemyEntity.CurrentLane = lane;
            enemyEntity.EnemyView = enemyView;
            enemyEntity.Transform = enemyView.transform;
            enemyEntity.Collider = enemyView.GetComponent<Collider>();
            enemyEntity.StateMachine = new EnemyStateMachine(new EnemyDirectionSystem(enemyEntity));
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyFilter.Value)
            {
                var enemy = _enemyPool.Value.Get(entity);
                enemy.StateMachine.Update();
            }
        }

        public void DestroyEnemy(EnemyView enemyView)
        {
            _enemyPool.Value.Del(enemyView.Entity);
            _enemies.ReturnInPool(enemyView);
        }
    }
}