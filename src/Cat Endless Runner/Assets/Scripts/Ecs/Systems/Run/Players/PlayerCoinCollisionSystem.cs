using Ecs.Events;
using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run.Players
{
    public class PlayerCoinCollisionSystem : IEcsRunSystem
    {
        private EcsPoolInject<PlayerCoinCollisionEvent> _playerCoinEventPool;
        private EcsPoolInject<Components.Player> _playersPool;
        private EcsFilterInject<Inc<PlayerCoinCollisionEvent>> _playerCollisionEventFilter;
        private EcsCustomInject<GameSceneData> _sceneData;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerCollisionEventFilter.Value)
            {
                ref var collisionEvent = ref _playerCoinEventPool.Value.Get(entity);
                ref var player = ref _playersPool.Value.Get(collisionEvent.PlayerEntity);

                player.Points += 1;
                collisionEvent.CoinView.Pool.ReturnInPool(collisionEvent.CoinView.transform);
                _sceneData.Value.PointsView.SetText("SCORE: " + player.Points);
            }
        }
    }
}