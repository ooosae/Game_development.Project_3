using Ecs.FSM;

namespace Ecs.Enemy.States
{
    public class AvoidObstacleState : BaseState
    {
        private readonly EnemyDirectionSystem _enemyDirectionSystem;

        public AvoidObstacleState(EnemyDirectionSystem enemyDirectionSystem)
        {
            _enemyDirectionSystem = enemyDirectionSystem;
        }

        public override void Execute()
        {
            if (_enemyDirectionSystem.IsObstacleAhead())
            {
                _enemyDirectionSystem.ChangeLane();
            }
            _enemyDirectionSystem.MoveForward();
        }
    }
}