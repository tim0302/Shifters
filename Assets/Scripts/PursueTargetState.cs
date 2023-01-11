using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;
        public RotateTowardsTargetState rotateTowardsTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);


            if (viewableAngle > 65 || viewableAngle < -65)
                return rotateTowardsTargetState;
            HandleRotationTowardsTarget(enemyManager);

            if (enemyManager.isInteracting)
            {
                return this;
            }

            if (enemyManager.isPreformingAction)
            {
                enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            // enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
            // enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if (distanceFromTarget <= enemyManager.maximumAggroRadius)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
        }

        private void HandleRotationTowardsTarget(EnemyManager enemyManager)
        {
            // if (enemyManager.isPreformingAction)
            // {
            //     Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            //     direction.y = 0;
            //     direction.Normalize();
            //     if (direction == Vector3.zero)
            //     {
            //         direction = transform.forward;
            //     }

            //     Quaternion targetRotation = Quaternion.LookRotation(direction);
            //     enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            // }
            // //rotate with pathfinding (navmesh)
            // else
            // {
            //     Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            //     Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

            //     enemyManager.navMeshAgent.enabled = true;
            //     enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            //     enemyManager.enemyRigidbody.velocity = targetVelocity;
            //     enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            // }
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
