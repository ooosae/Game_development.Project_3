using UnityEngine;

namespace Ecs.Components
{
    public struct Player
    {
        public Transform Transform;
        public Animator Animator;
        public Rigidbody Rigidbody;
        public float MoveSpeed;
        public float Distance { get; set; }
        public int Points;
        public bool IsAlive;
        public bool IsBoosted;
    }
}
