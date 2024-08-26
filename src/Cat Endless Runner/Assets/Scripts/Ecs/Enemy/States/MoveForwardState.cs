using Ecs.FSM;

namespace Ecs.Enemy.States
{
    public class MoveForwardState : BaseState
    {
        private readonly EnemyDirectionSystem _enemyDirectionSystem;

        public MoveForwardState(EnemyDirectionSystem enemyDirectionSystem)
        {
            _enemyDirectionSystem = enemyDirectionSystem;
        }

        public override void Execute()
        {
            if (!_enemyDirectionSystem.IsObstacleAhead())
            {
                _enemyDirectionSystem.MoveForward();
            }
        }
    }
}