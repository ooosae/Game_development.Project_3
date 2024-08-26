using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems.Run
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameSceneData> _sceneData; 
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;

        private Vector3 _offset;
        private Camera _camera;
        
        public void Init(IEcsSystems systems)
        {
            _camera = Camera.main;
            if (_camera != null)
                _offset = _camera.transform.position - _sceneData.Value.PlayerEntity.transform.position;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var player = _playerPool.Value.Get(entity);
                
                var targetPosition = player.Transform.position + _offset;
                targetPosition.x = 0;
                targetPosition.y = 8;
                _camera.transform.position = targetPosition;
                return;
            }
        }
    }
}