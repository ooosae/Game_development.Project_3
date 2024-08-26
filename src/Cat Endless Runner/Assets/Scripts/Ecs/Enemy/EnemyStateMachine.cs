using System.Collections.Generic;
using Ecs.Enemy.States;
using Ecs.FSM;

namespace Ecs.Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        public EnemyStateMachine(EnemyDirectionSystem enemyDirectionSystem)
        {
            var moveForwardState = new MoveForwardState(enemyDirectionSystem);
            var avoidObstacleState = new AvoidObstacleState(enemyDirectionSystem);

            SetInitialState(moveForwardState);

            AddState(moveForwardState, new List<Transition>
            {
                new Transition(
                    avoidObstacleState,
                    enemyDirectionSystem.IsObstacleAhead)
            });

            AddState(avoidObstacleState, new List<Transition>
            {
                new Transition(
                    moveForwardState,
                    () => !enemyDirectionSystem.IsObstacleAhead())
            });
        }
    }
}