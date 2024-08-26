using Ecs.Components;
using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;

namespace Ecs.Systems.Init
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<Player> _playerPool;
        private EcsPoolInject<PlayerTag> _playerTagPool;
        private EcsPoolInject<PlayerInputData> _playerInputDataPool;
        private EcsCustomInject<GameSettings> _gameSettings;
        private EcsCustomInject<GameSceneData> _sceneData;

        private int _playerEntity;

        public void Init(IEcsSystems systems)
        {
            _playerEntity = _world.Value.NewEntity();
            
            _playerTagPool.Value.Add(_playerEntity);
            _playerInputDataPool.Value.Add(_playerEntity);
            ref var player = ref _playerPool.Value.Add(_playerEntity);

            _sceneData.Value.PlayerEntity.Init(_playerEntity, _world.Value);
            
            player.IsAlive = true;
            player.IsBoosted = false;
            player.MoveSpeed = _gameSettings.Value.PlayerMovementSpeed;
            player.Rigidbody = _sceneData.Value.PlayerEntity.GetComponent<Rigidbody>();
            player.Transform = _sceneData.Value.PlayerEntity.GetComponent<Transform>();
            player.Animator = _sceneData.Value.PlayerEntity.GetComponent<Animator>();
        }
    }
}