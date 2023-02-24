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
                    animatorHandler.PlayTargetAnimation(weapon.Light_Attack_2, true);
                    characterEffectManager.PlayWeaponFX(false);
                    lastAttack = weapon.Light_Attack_2;
                    return;
                }

                if (lastAttack == weapon.Light_Attack_2)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Light_Attack_3, true);
                    characterEffectManager.PlayWeaponFX(false);
                    lastAttack = weapon.Light_Attack_3;
                    return;
                }

                if (lastAttack == weapon.Light_Attack_3)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Light_Attack_4, true);
                    characterEffectManager.PlayWeaponFX(false);
                    lastAttack = weapon.Light_Attack_4;
                    return;
                }
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                animatorHandler.PlayTargetAnimation(weapon.Light_Attack_1, true);
                characterEffectManager.PlayWeaponFX(false);
                lastAttack = weapon.Light_Attack_1;
            }
            if (animatorHandler.animator.GetBool("canDoRollAttack"))
            {
                animatorHandler.animator.SetBool("canDoRollAttack", false);
                characterEffectManager.PlayWeaponFX(false);
                animatorHandler.PlayTargetAnimation(weapon.Heavy_Attack, true);
            }
        }

        public void HandleParry(WeaponItem weapon)
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                animatorHandler.PlayTargetAnimation("parry", true);
            }
        }

        public void HandleSpecialAttack(WeaponItem weapon)
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                animatorHandler.PlayTargetAnimation(weapon.Special_Attack, true);
                characterEffectManager.PlayWeaponFX(true);
            }
        }
    }
}
