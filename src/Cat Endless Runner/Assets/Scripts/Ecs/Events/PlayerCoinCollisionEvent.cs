using Ecs.Views;

namespace Ecs.Events
{
    public struct PlayerCoinCollisionEvent
    {
        public int PlayerEntity;
        public CoinView CoinView;
    }
}