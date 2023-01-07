using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        InputHandler inputHandler;
        public static CameraHandler singleton;
        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;
        public float maximumLockOnDistance = 30;
        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Transform currentLockOnTarget;
        public Transform nearestLockOnTarget;
        private float defaultPostion;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;
        public float lockedPivotPosition = 1.85f;
        public float unlockedPivotPosition = 1.65f;

        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

            if (currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
            }
            // else
            // {
            //     cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
            // }
        }
        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPostion = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            inputHandler = FindObjectOfType<InputHandler>();
        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterManager = colliders[i].GetComponent<CharacterManager>();
                if (characterManager != null)
                {
                    Vector3 lockTargetDirection = characterManager.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, characterManager.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                    if (characterManager.transform.root != targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maximumLockOnDistance)
                    {
                        availableTargets.Add(characterManager);
                    }
                }
            }

            for (int j = 0; j < availableTargets.Count; j++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[j].transform.position);

                if (distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[j].lockOnTransform;
                }
            }
        }
        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
            myTransform.position = targetPosition;
        }

        public void ClearLockOnTargets()
        {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
        }
        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            if (inputHandler.lockOnFlag == false && currentLockOnTarget == null)
            {
                lookAngle += (mouseXInput * lookSpeed) / delta;
                pivotAngle -= (mouseYInput * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotation;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;
                targetRotation = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotation;
            }
            else
            {
                float velocity = 0;
                Vector3 direction = currentLockOnTarget.position - transform.position;
                direction.Normalize();
                direction.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
                direction = currentLockOnTarget.position - cameraPivotTransform.position;
                direction.Normalize();

                targetRotation = Quaternion.LookRotation(direction);
                Vector3 eulerAngle = targetRotation.eulerAngles;
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }

        }
    }
}

