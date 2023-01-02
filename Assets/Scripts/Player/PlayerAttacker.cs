using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public string lastAttack;
        CharacterEffectManager characterEffectManager;
        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            characterEffectManager = GetComponent<CharacterEffectManager>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.animator.SetBool("canDoCombo", false);
                if (lastAttack == weapon.Light_Attack_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.Light_Attack_2, true);
                    characterEffectManager.PlayWeaponFX(false);

                }
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                animatorHandler.PlayerTargetAnimation(weapon.Light_Attack_1, true);
                characterEffectManager.PlayWeaponFX(false);
                lastAttack = weapon.Light_Attack_1;
            }
            if (animatorHandler.animator.GetBool("canDoRollAttack"))
            {
                animatorHandler.animator.SetBool("canDoRollAttack", false);
                characterEffectManager.PlayWeaponFX(false);
                animatorHandler.PlayerTargetAnimation(weapon.Heavy_Attack, true);
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                animatorHandler.PlayerTargetAnimation(weapon.Special_Attack, true);
                characterEffectManager.PlayWeaponFX(true);
            }
        }
    }
}
