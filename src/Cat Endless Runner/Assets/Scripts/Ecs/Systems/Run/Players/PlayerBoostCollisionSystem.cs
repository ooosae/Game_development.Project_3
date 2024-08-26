using Ecs.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run.Players
{
    public class PlayerBoostCollisionSystem : IEcsRunSystem
    {
        private EcsPoolInject<PlayerBoostCollisionEvent> _playerBoostEventPool;
        private EcsPoolInject<Components.Player> _playersPool;
        private EcsFilterInject<Inc<PlayerBoostCollisionEvent>> _playerCollisionEventFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerCollisionEventFilter.Value)
            {
                ref var collisionEvent = ref _playerBoostEventPool.Value.Get(entity);
                ref var player = ref _playersPool.Value.Get(collisionEvent.PlayerEntity);

                player.IsBoosted = true;
                collisionEvent.BoostView.Pool.ReturnInPool(collisionEvent.BoostView.transform);
            }
        }
    }
}