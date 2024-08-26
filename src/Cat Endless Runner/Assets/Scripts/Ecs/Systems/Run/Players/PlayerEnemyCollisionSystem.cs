using Ecs.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run.Players
{
    public class PlayerEnemyCollisionSystem : IEcsRunSystem
    {
        private EcsPoolInject<PlayerEnemyCollisionEvent> _playerCollisionEventPool;
        private EcsPoolInject<Components.Player> _playersPool;
        private EcsFilterInject<Inc<PlayerEnemyCollisionEvent>> _playerCollisionEventFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerCollisionEventFilter.Value)
            {
                ref var collisionEvent = ref _playerCollisionEventPool.Value.Get(entity);
                ref var player = ref _playersPool.Value.Get(collisionEvent.PlayerEntity);

                if (player.IsBoosted)
                {
                    player.IsBoosted = false;
                    collisionEvent.EntityView.Pool.ReturnInPool(collisionEvent.EntityView.transform);
                }
                else
                {
                    player.IsAlive = false;
                }
            }
        }
    }
}