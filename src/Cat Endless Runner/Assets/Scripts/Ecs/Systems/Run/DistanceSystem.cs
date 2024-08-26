using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems.Run
{
    public class DistanceSystem : IEcsRunSystem
    {
        private const float IncrementInterval = 0.1f;
        
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;
        private EcsCustomInject<GameSceneData> _sceneData;
        
        private float _timer;

        public void Run(IEcsSystems systems)
        {
            _timer += Time.deltaTime;
            if (_timer < IncrementInterval)
            {
                return;
            }
            
            _timer = 0f;   
            
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                if (!player.IsAlive)
                {
                    continue;
                }
                
                player.Distance += 1;
                _sceneData.Value.DistanceView.SetText("DISTANCE: " + player.Distance);
            }
        }
    }
}