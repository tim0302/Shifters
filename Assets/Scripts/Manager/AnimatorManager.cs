using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator animator;
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.SetBool("isRotatingWithRootMotion", true);
            animator.CrossFade(targetAnim, 0.2f);
        }
    }
}
