using Ecs.Extensions;
using Ecs.Services;
using Ecs.Systems.Init;
using Ecs.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;
using CoroutineRunner = Ecs.Services.CoroutineRunner;

namespace Ecs.Systems.Logic
{
    public class TileCreator : IEcsInitSystem
    {
        private const int CoinsToSpawn = 2;
        private const float TimeToDestroyTile = 2;
        private const int TileLength = 10;
        private const float ChanceToSpawnBoost = 50f;
        
        private EcsCustomInject<GameSceneData> _sceneData; 
        private EcsCustomInject<EnemyCreator> _enemyCreator; 
        private EcsCustomInject<CoroutineRunner> _coroutineRunner; 
        private EcsCustomInject<TileItemsCreator> _tileItemsController;

        private readonly ChanceHelper _chanceHelper = new();
        
        private Vector3 _nextSpawnPoint;
        private Pool<GroundTileView> _tilePool;

        public void Init(IEcsSystems systems)
        {
            _nextSpawnPoint = _sceneData.Value.StartTilePosition.position;
            var tilePrefab = _sceneData.Value.GroundTileView;

            _tilePool = new Pool<GroundTileView>(tilePrefab);
        }

        public void DestroyTile(GroundTileView tileView)
        {
            _coroutineRunner.Value.TimerExecute(TimeToDestroyTile, () =>
            {
                foreach (var item in tileView.ContainedItems)
                {
                    item.Pool.ReturnInPool(item.transform);
                }
                
                tileView.ContainedItems.Clear();
                
                _tilePool.ReturnInPool(tileView);
            });
        }

        public void SpawnTile(bool isEmpty = false)
        {
            var groundTile = _tilePool.Get();
            groundTile.transform.position = _nextSpawnPoint;
            var newNextSpawnPoint = _nextSpawnPoint;
            newNextSpawnPoint.z += TileLength;
            _nextSpawnPoint = newNextSpawnPoint;
            
            if (!isEmpty)
            {
                var tileType = Random.Range(0, 4);
                if (tileType == 0)
                {
                    SpawnFlowers(groundTile);
                } 
                else if (tileType == 1)
                {
                    SpawnDog(groundTile);
                }
                else if (tileType == 2)
                {
                    SpawnBird(groundTile);
                }
                else
                {
                    SpawnEnemies(groundTile);
                }
            }
            
            SpawnCoins(groundTile);
            SpawnBoost(groundTile);
        }

        private int GetRandomLane(GroundTileView groundTile)
        {
            return Random.Range(0, groundTile.ObstaclePoints.Count);
        }
        
        private Vector3 GetLanePosition(GroundTileView groundTile, int index)
        {
            return groundTile.ObstaclePoints[index].position;
        }
        
        private Vector3 GetRandomObstaclePosition(GroundTileView groundTile)
        {
            return groundTile.ObstaclePoints.GetRandomValue().position;
        }
        
        private void SpawnBird(GroundTileView groundTile)
        {
            var spawnPosition = GetRandomObstaclePosition(groundTile) - new Vector3(0f, -1.3f, 0f);
            var spawnRotation = Quaternion.Euler(0f, 180f, 0f);

            var entity = _tileItemsController.Value.CreateEntity(EntityType.Bird);
            entity.position = spawnPosition;
            entity.rotation = spawnRotation;
            groundTile.ContainedItems.Add(entity.GetComponent<TileItemView>());
        }

        private void SpawnDog(GroundTileView groundTile)
        {
            var spawnPosition = GetRandomObstaclePosition(groundTile) - new Vector3(0f, 0.5f, 0f);
            var spawnRotation = Quaternion.Euler(0f, 180f, 0f);

            var entity = _tileItemsController.Value.CreateEntity(EntityType.Dog);
            entity.position = spawnPosition;
            entity.rotation = spawnRotation;
            groundTile.ContainedItems.Add(entity.GetComponent<TileItemView>());
        }

        private void SpawnEnemies(GroundTileView groundTile)
        {
            var lane = GetRandomLane(groundTile);
            var spawnPosition = GetLanePosition(groundTile, lane);
            _enemyCreator.Value.CreateEnemy(spawnPosition, lane);
        }

        private void SpawnFlowers(GroundTileView groundTile)
        {
            var spawnPosition = GetRandomObstaclePosition(groundTile);

            var entity = _tileItemsController.Value.CreateEntity(EntityType.Flower);
            entity.position = spawnPosition;
            
            var boxCollider = entity.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(1, 1, 1);
            groundTile.ContainedItems.Add(entity.GetComponent<TileItemView>());
        }

        private void SpawnCoins(GroundTileView groundTile)
        {
            for (var i = 0; i < CoinsToSpawn; ++i)
            {
                SpawnCoin(groundTile);
            }
        }

        private void SpawnCoin(GroundTileView groundTile)
        {
            var coin = _tileItemsController.Value.CreateCoin();
            
            var coinCollider = coin.gameObject.AddComponent<BoxCollider>();
            coinCollider.isTrigger = true;
            PlaceOnRandomPoint(groundTile, coin);
            groundTile.ContainedItems.Add(coin.GetComponent<TileItemView>());
        }

        private void SpawnBoost(GroundTileView groundTile)
        {
            if (_chanceHelper.CalculateChance(ChanceToSpawnBoost))
            {
                return;
            }
            
            var boost = _tileItemsController.Value.CreateBoost();
            
            var boostCollider = boost.gameObject.AddComponent<BoxCollider>();
            boostCollider.isTrigger = true;
            PlaceOnRandomPoint(groundTile, boost);
            groundTile.ContainedItems.Add(boost.GetComponent<TileItemView>());
        }

        private void PlaceOnRandomPoint(GroundTileView groundTile, Transform item)
        {
            item.transform.position = GetRandomPointInCollider(groundTile.GetComponent<Collider>());
            item.transform.position += groundTile.Center.position;
        }
        
        private Vector3 GetRandomPointInCollider(Collider collider)
        {
            var bounds = collider.bounds;
            var point = new Vector3(
                Random.Range(-bounds.extents.x, bounds.extents.x),
                Random.Range(-bounds.extents.y, bounds.extents.y),
                Random.Range(-bounds.extents.z, bounds.extents.z));

            point.y = 1;
            
            return point;
        }
    }
}