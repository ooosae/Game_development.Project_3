using System.Collections.Generic;
using Ecs.Enemy;
using Ecs.Services;
using Ecs.Views;
using Ecs.Views.GameUI;
using UnityEngine;

namespace Ecs
{
    public class GameSceneData : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerEntity;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [Space] 
        [SerializeField] private GameObject _birdPrefab;
        [SerializeField] private GameObject _dogPrefab;
        [SerializeField] private EnemyView _leoPrefab;
        [SerializeField] private GameObject _boostPrefab;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private List<GameObject> _flowerPrefabs;
        [Space] 
        [SerializeField] private Transform _startTilePosition;
        [SerializeField] private GroundTileView _groundTileView;
        [Space]
        [SerializeField] private DistanceView _distanceView;
        [SerializeField] private PointsView _pointsView;
        
        public PlayerView PlayerEntity => _playerEntity;
        public CoroutineRunner CoroutineRunner => _coroutineRunner;
        
        public GameObject BirdPrefab => _birdPrefab;
        public GameObject DogPrefab => _dogPrefab;
        public EnemyView LeoPrefab => _leoPrefab;
        public GameObject BoostPrefab => _boostPrefab;
        public GameObject CoinPrefab => _coinPrefab;
        public List<GameObject> FlowerPrefabs => _flowerPrefabs;
        
        public Transform StartTilePosition => _startTilePosition;
        public GroundTileView GroundTileView => _groundTileView;
        
        public DistanceView DistanceView => _distanceView;
        public PointsView PointsView => _pointsView;
    }
}