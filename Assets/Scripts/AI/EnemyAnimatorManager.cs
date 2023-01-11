using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyStats enemyStats;
        EnemyBossManager enemyBossManager;
        public GameObject effectTransform;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemyStats = GetComponent<EnemyStats>();
            enemyManager = GetComponent<EnemyManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
        }
        public void EnableIsInvulnerable()
        {

        }
        public void InstantiateBossParticleFX()
        {
            GameObject phaseFX = Instantiate(enemyBossManager.phaseChangeFX, effectTransform.transform);
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;

            if (enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= animator.deltaRotation;
            }
        }
    }
}
