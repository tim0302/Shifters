using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shifters
{
    public class AnimatorHandler : MonoBehaviour
    {

        public Animator animator;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        int vertical;
        int horizontal;
        public bool canRotate;
        public void Initialise()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            inputHandler = GetComponentInParent<InputHandler>();

        }
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion 

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotation()
        {
            canRotate = false;
        }

        public void PlayerTargetAnimation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }

        //readjust the model upon animation play
        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
            {
                return;
            }

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;
        }
        public void EnableCombo()
        {
            animator.SetBool("canDoCombo", true);

        }

        public void DisableCombo()
        {
            animator.SetBool("canDoCombo", false);
        }
    }
}