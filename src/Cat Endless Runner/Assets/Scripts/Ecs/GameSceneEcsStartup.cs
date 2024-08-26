using Ecs.Events;
using Ecs.LeaderBord;
using Ecs.Services;
using Ecs.Systems.Init;
using Ecs.Systems.Logic;
using Ecs.Systems.Run;
using Ecs.Systems.Run.Players;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Settings;
using UnityEngine;

namespace Ecs
{
    sealed class GameSceneEcsStartup : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private GameSceneData _gameSceneData;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        void Start()
        {
            var tileItemsController = new TileItemsCreator();
            var tileCreator = new TileCreator();
            var enemyCreator = new EnemyCreator();
            var leaderBordDataController = new LeaderBordDataController();
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new PlayerInitSystem())
                .Add(tileItemsController)
                .Add(tileCreator)
                .Add(enemyCreator)
                .Add(new StartTilesCreator())
                .Add(new CameraFollowSystem())
                .Add(new DistanceSystem())
                
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new PlayerFallSystem())
                .Add(new EnemyFallSystem())
                
                .Add(new PlayerEnemyCollisionSystem())
                .Add(new PlayerCoinCollisionSystem())
                .Add(new PlayerBoostCollisionSystem())
                .Add(new PlayerTileCollisionExitSystem())
                
                .Add(new DefeatSystem())
                .DelHere<PlayerEnemyCollisionEvent>()
                .DelHere<PlayerBoostCollisionEvent>()
                .DelHere<PlayerCoinCollisionEvent>()
                .DelHere<PlayerTileCollisionExitEvent>()
                .Inject(_gameSettings)
                .Inject(_gameSceneData)
                .Inject(_gameSceneData.CoroutineRunner)
                .Inject(tileItemsController)
                .Inject(tileCreator)
                .Inject(enemyCreator)
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