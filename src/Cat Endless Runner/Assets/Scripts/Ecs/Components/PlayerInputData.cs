using UnityEngine;

namespace Ecs.Components
{
    public struct PlayerInputData
    {
        public Vector3 MoveInput;
        public bool IsJump;
        public bool IsSlide;
        public float ActionCooldown;
    }
}