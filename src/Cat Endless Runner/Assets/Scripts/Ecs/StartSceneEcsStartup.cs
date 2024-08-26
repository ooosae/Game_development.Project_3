using Ecs.LeaderBord;
using Ecs.Systems.Defeat;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;

namespace Ecs
{
    public class StartSceneEcsStartup : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private StartSceneData _startSceneData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        void Start()
        {
            var leaderBordDataController = new LeaderBordDataController();
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new DefeatToStartSceneSystem())
                .Add(new LeaderBordController())
                .Inject(_gameSettings)
                .Inject(_startSceneData)
                .Inject(_startSceneData.CoroutineRunner)
                .Inject(leaderBordDataController)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}