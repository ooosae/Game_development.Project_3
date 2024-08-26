using Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems.Run.Players
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private static readonly int Boost1 = Animator.StringToHash("Boost");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Slide = Animator.StringToHash("Slide");
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsPoolInject<PlayerInputData> _playerInputDataPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var player = _playerPool.Value.Get(entity);
                var playerInputData = _playerInputDataPool.Value.Get(entity);
                
                player.Animator.SetInteger(Walk, 1);
                
                if (playerInputData.IsJump)
                {
                    player.Animator.SetTrigger(Jump);
                }

                if (playerInputData.IsSlide)
                {
                    player.Animator.SetTrigger(Slide);
                }
            }
        }
    }
}