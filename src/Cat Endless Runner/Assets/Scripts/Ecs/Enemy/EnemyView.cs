using UnityEngine;

namespace Ecs.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyView : MonoBehaviour
    {
        public int Entity { get; private set; }

        public void Init(int entity)
        {
            Entity = entity;
        }
    }
}