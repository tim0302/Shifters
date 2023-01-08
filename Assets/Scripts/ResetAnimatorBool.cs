using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string isInteracting = "isInteracting";
        public bool isInteractingStatus = false;
        public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
        public string canDoRollAttack = "canDoRollAttack";
        public bool canDoRollAttackStatus = false;
        public bool isRotatingWithRootMotionStatus = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
            animator.SetBool(isInteracting, isInteractingStatus);
            animator.SetBool(canDoRollAttack, canDoRollAttackStatus);

        }
    }
}
