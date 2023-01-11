using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public EnemyAttackAction currentAttack;
        public RotateTowardsTargetState rotateTowardsTargetState;
        public PursueTargetState pursueTargetState;
        public bool hasPerformedAttack = false;
        EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponentInParent<EnemyManager>();
        }
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            RotateTowardsTargetWhilestAttacking(enemyManager);
            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if (!hasPerformedAttack && !enemyManager.isInteracting)
            {
                AttackTarget(enemyAnimatorManager, enemyManager);
            }

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
        {
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
            hasPerformedAttack = true;
        }

        private void RotateTowardsTargetWhilestAttacking(EnemyManager enemyManager)
        {
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
