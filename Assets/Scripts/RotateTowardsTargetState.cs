using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class RotateTowardsTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            print("EnterRotate");

            enemyAnimatorManager.animator.SetFloat("Vertical", 0);
            enemyAnimatorManager.animator.SetFloat("Horizontal", 0);

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            if (enemyManager.isInteracting)
                return combatStanceState;
            if (viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Back Turn", true);
                return combatStanceState;
            }
            else if (viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Back Turn", true);
                return combatStanceState;

            }
            else if (viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Right Turn", true);
                return combatStanceState;

            }
            else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Left Turn", true);
                return combatStanceState;
            }
            return combatStanceState;
        }
    }
}
