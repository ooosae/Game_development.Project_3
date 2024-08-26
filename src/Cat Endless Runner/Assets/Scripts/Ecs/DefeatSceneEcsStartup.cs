using Ecs.Systems.Defeat;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Settings;
using UnityEngine;

namespace Ecs
{
    public class DefeatSceneEcsStartup : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private DefeatSceneData _defeatSceneData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new DefeatToStartSceneSystem())
                .Inject(_gameSettings)
                .Inject(_defeatSceneData)
                .Inject(_defeatSceneData.CoroutineRunner)
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