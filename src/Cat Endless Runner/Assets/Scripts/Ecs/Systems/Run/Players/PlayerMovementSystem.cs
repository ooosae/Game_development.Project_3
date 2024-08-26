using Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;

namespace Ecs.Systems.Run.Players
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsPoolInject<PlayerInputData> _playerInputDataPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;
        private EcsCustomInject<GameSettings> _gameSettings;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var player = _playerPool.Value.Get(entity);
                var playerInputData = _playerInputDataPool.Value.Get(entity);

                if (!player.IsAlive)
                {
                    return;
                }
                
                var horizontalMovement = playerInputData.MoveInput * _gameSettings.Value.PlayerHorizontalSpeed;
                var verticalMovement = Vector3.forward * player.MoveSpeed;
                var movement = (horizontalMovement + verticalMovement) * Time.deltaTime;
                
                player.Transform.Translate(movement, Space.World);

                if (playerInputData.IsJump)
                {
                    player.Rigidbody.AddForce(0, _gameSettings.Value.PlayerJumpForce, 0);
                }
            }
        }
    }
}