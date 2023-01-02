using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Shifters
{
    public class EnemyManager : CharacterManager
    {
        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public float rotationSpeed = 15;
        public float maximumAttackRange = 10f;
        EnemyStats enemyStats;
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public bool isPreformingAction;
        public Rigidbody enemyRigidbody;

        [Header("AI setting")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float currentRecoveryTime = 0;
        public float viewableAngle;
        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
            enemyRigidbody = GetComponent<Rigidbody>();

        }
        private void Start()
        {
            enemyRigidbody.isKinematic = false;
        }

        // Update is called once per frame
        private void Update()
        {
            HandleStateMachine();
            HandleRecoveryTimer();
        }

        private void FixedUpdate()
        {

        }

        private void HandleStateMachine()
        {
            if (enemyStats.isDead)
            {
                return;
            }
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void OnDrawGizmosSelected()
        {
            // Gizmos.color = Color.red; 
            // Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }

    }
}
