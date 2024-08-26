using Ecs.Constants;
using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ecs.Systems.Defeat
{
    public class DefeatToStartSceneSystem : IEcsInitSystem
    {
        private EcsCustomInject<GameSettings> _gameSettings;
        private EcsCustomInject<DefeatSceneData> _defeatSceneData;
        private EcsCustomInject<CoroutineRunner> _coroutineRunner;
        
        public void Init(IEcsSystems systems)
        {
            _coroutineRunner.Value.TimerExecute(_gameSettings.Value.DefeatPanelDuration, MainScene);
        }

        private void MainScene()
        {
            SceneManager.LoadScene(SceneConstants.StartScene);
        }
    }
}