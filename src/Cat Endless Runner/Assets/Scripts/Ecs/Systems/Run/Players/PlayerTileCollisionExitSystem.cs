using Ecs.Events;
using Ecs.Systems.Logic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run.Players
{
    public class PlayerTileCollisionExitSystem : IEcsRunSystem
    {
        private EcsCustomInject<TileCreator> _tileCreator;
        private EcsPoolInject<PlayerTileCollisionExitEvent> _playerTileCollisionExitEventPool;
        private EcsFilterInject<Inc<PlayerTileCollisionExitEvent>> _playerTileCollisionExitEventPoolFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerTileCollisionExitEventPoolFilter.Value)
            {
                ref var collisionEvent = ref _playerTileCollisionExitEventPool.Value.Get(entity);
                
                _tileCreator.Value.DestroyTile(collisionEvent.GroundTileView);
                _tileCreator.Value.SpawnTile();
            }
        }
    }
}