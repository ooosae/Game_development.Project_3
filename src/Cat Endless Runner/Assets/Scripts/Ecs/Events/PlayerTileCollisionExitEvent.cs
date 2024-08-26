using Ecs.Views;

namespace Ecs.Events
{
    public struct PlayerTileCollisionExitEvent
    {
        public int PlayerEntity;
        public GroundTileView GroundTileView;
    }
}