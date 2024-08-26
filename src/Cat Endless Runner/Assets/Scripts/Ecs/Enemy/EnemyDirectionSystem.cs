using UnityEngine;

namespace Ecs.Enemy
{
    public class EnemyDirectionSystem
    {
        private readonly float[] _lanes = { -3.3f, 0f, 3.3f };
        
        private const float MinRunSpeed = 2f;
        private const float MaxRunSpeed = 7f;
        private Ecs.Components.Enemy _enemy;

        public EnemyDirectionSystem(Ecs.Components.Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public void MoveForward()
        {
            var speed = Random.Range(MinRunSpeed, MaxRunSpeed);
            _enemy.Transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        public void ChangeLane()
        {
            _enemy.CurrentLane = _enemy.CurrentLane switch
            {
                0 or 2 => 1,
                1 => Random.value > 0.5f ? 0 : 2,
                _ => _enemy.CurrentLane
            };

            var newPosition = new Vector3(_lanes[_enemy.CurrentLane], _enemy.Transform.position.y, _enemy.Transform.position.z);
            _enemy.Transform.position = newPosition;
        }

        public bool IsObstacleAhead()
        {
            var rayOrigin = new Vector3(_enemy.Transform.position.x, _enemy.Collider.bounds.center.y, _enemy.Transform.position.z);
            return Physics.Raycast(rayOrigin, Vector3.forward, out var hit, 5.0f) &&
                   hit.collider.CompareTag("Obstacle");
        }
    }
}
