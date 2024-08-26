using Ecs.Views;

namespace Ecs.Events
{
    public struct PlayerEnemyCollisionEvent
    {
        public int PlayerEntity;
        public EntityView EntityView;
    }
}