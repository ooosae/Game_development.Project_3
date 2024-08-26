using Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;

namespace Ecs.Systems.Run.Players
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsPoolInject<PlayerInputData> _playerInputDataPool;
        private EcsFilterInject<Inc<PlayerInputData>> _playerInputDataFilter;
        private EcsCustomInject<GameSettings> _gameSettings;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerInputDataFilter.Value)
            {
                ref var playerInputData = ref _playerInputDataPool.Value.Get(entity);

                var moveHorizontal = Input.GetAxisRaw("Horizontal");
                playerInputData.MoveInput = new Vector3(moveHorizontal, 0.0f, 0.0f);
                playerInputData.IsJump = false;
                playerInputData.IsSlide = false;

                playerInputData.ActionCooldown -= Time.deltaTime;
                if (playerInputData.ActionCooldown <= 0.0f)
                {
                    playerInputData.ActionCooldown = 0.0f;
                    
                    if (Input.GetButtonDown("Slide"))
                    {
                        playerInputData.IsSlide = true;
                        playerInputData.ActionCooldown = _gameSettings.Value.PlayerTimeBeforeNextSlide;
                    }

                    if (Input.GetButtonDown("Jump"))
                    {
                        playerInputData.IsJump = true;
                        playerInputData.ActionCooldown = _gameSettings.Value.PlayerTimeBeforeNextJump;
                    }
                }
            }
        }
    }
}