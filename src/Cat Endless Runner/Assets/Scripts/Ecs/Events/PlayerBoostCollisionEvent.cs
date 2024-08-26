using Ecs.Views;

namespace Ecs.Events
{
    public struct PlayerBoostCollisionEvent
    {
        public int PlayerEntity;
        public BoostView BoostView;
    }
}