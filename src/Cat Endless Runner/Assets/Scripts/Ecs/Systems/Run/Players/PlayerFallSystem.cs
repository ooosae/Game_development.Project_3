using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Run.Players
{
    public class PlayerFallSystem : IEcsRunSystem
    {
        private const float LowerBound = -2.0f;
        
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                
                if (player.Transform.position.y < LowerBound)
                {
                    player.IsAlive = false;
                }
            }
        }
    }
}