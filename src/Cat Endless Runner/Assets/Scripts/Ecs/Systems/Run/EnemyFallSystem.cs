using Ecs.Systems.Logic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run
{
    public class EnemyFallSystem : IEcsRunSystem
    {
        private const float LowerBound = -2.0f;

        private EcsCustomInject<EnemyCreator> _enemyCreator;
        private EcsPoolInject<Components.Enemy> _enemyPool;
        private EcsFilterInject<Inc<Components.Enemy>> _enemyFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyFilter.Value)
            {
                var enemy = _enemyPool.Value.Get(entity);
                
                if (enemy.EnemyView.transform.position.y < LowerBound)
                {
                    _enemyCreator.Value.DestroyEnemy(enemy.EnemyView);
                }
            }
        }
    }
}