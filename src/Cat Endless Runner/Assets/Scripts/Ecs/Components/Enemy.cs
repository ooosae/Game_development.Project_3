using Ecs.Enemy;
using UnityEngine;

namespace Ecs.Components
{
    public struct Enemy
    {
        public EnemyView EnemyView;
        public Transform Transform;
        public Collider Collider;
        public int CurrentLane;
        public EnemyStateMachine StateMachine;
    }
}