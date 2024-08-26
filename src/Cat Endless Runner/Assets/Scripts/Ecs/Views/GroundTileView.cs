using System.Collections.Generic;
using UnityEngine;

namespace Ecs.Views
{
    public class GroundTileView : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Transform _nextSpawnPoint;
        [SerializeField] private List<Transform> _obstaclePoints;

        public List<TileItemView> ContainedItems { get; set; } = new List<TileItemView>();
        
        public Transform Center => _center;
        public Transform NextSpawnPoint => _nextSpawnPoint;
        public List<Transform> ObstaclePoints => _obstaclePoints;
    }
}