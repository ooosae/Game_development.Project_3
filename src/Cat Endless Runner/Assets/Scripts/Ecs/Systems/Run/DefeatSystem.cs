using System.Collections;
using Ecs.Constants;
using Ecs.LeaderBord;
using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ecs.Systems.Run
{
    public class DefeatSystem : IEcsRunSystem
    {
        private EcsCustomInject<LeaderBordDataController> _leaderBordDataController;
        private EcsPoolInject<Components.Player> _playerPool;
        private EcsFilterInject<Inc<Components.Player>> _playerFilter;
        private EcsCustomInject<CoroutineRunner> _coroutineRunner;

        private bool _isLoadDefeatScene = false;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var player = _playerPool.Value.Get(entity);
                
                if (!player.IsAlive)
                {
                    if (_isLoadDefeatScene)
                    {
                        return;
                    }

                    _leaderBordDataController.Value.AddLeaderBordResult((int)player.Distance);
                    _isLoadDefeatScene = true;
                    _coroutineRunner.Value.TimerExecute(1.0f, LoadDefeatScene);
                }
            }
        }

        private static void LoadDefeatScene()
        {
            SceneManager.LoadScene(SceneConstants.DefeatScene);
        }
    }
}