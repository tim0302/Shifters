using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shifters
{
    public class PlayerStats : CharacterStats
    {

        public HealthBar healthbar;
        public ManaBar manaBar;
        public Gradient gradient;
        Animator animator;
        Animator deathAnimator;
        AnimatorHandler animatorHandler;
        CharacterSoundFXManager characterSoundFXManager;
        CharacterEffectManager characterEffectManager;
        WeaponManager weaponManager;
        public PlayerManager playerManager;
        Image healthBar;
        public int playerSpecialAttackDamageMultiplier = 1;

        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            animator = GetComponentInChildren<Animator>();

            weaponManager = GetComponent<WeaponManager>();
            playerManager = GetComponent<PlayerManager>();
            characterEffectManager = GetComponent<CharacterEffectManager>();
            deathAnimator = GameObject.Find("YouDied").GetComponent<Animator>();
        }

        private void Update()
        {
            playerSpecialAttackDamageMultiplier = stamina / 10;
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            manaBar.SetMaxMana(this.maxStamina);
            var barFill = GameObject.Find("Health Bar Fill");
            healthBar = barFill.GetComponent<Image>();
        }
        public void GainStaminaByDamage(int damage)
        {
            // if ((stamina + damage / 4) <= maxStamina)
            // {
            stamina = Mathf.Clamp(stamina += damage / 4, 0, maxStamina);
            stamina += damage / 4;
            manaBar.SetCurrentMana(stamina);
            // }
            // else
            // {
            //     stamina = maxStamina;
            //     manaBar.SetCurrentMana(stamina);
            // }
        }

        public void GainStaminaByParry(int staminaGained)
        {
            stamina = Mathf.Clamp(stamina += staminaGained, 0, maxStamina);
            manaBar.SetCurrentMana(stamina);
            // if (stamina + staminaGained >= maxStamina)
            // {
            //     stamina = maxStamina;
            //     manaBar.SetCurrentMana(stamina);
            //     return;
            // }
            // stamina += staminaGained;
            // manaBar.SetCurrentMana(stamina);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void Heal()
        {
            if (!animator.GetBool("isInteracting") && !animator.GetBool("canDoRollAttack"))
            {
                // animatorHandler.PlayTargetAnimation("Heal", true);

                if (stamina == 0) { return; }

                //TODO: REFACTOR USE CLAMP
                currentHealth = Mathf.Clamp(currentHealth + (stamina * maxHealth) / 400, 0, maxHealth);
                healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);
                characterSoundFXManager.PlayHeal();
                characterEffectManager.PlayHealFX(playerManager.transform.position);
                // if ((currentHealth + (stamina * maxHealth) / 400) <= maxHealth)
                // {
                //     currentHealth += (stamina * maxHealth) / 400;
                //     healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);
                //     characterSoundFXManager.PlayHeal();
                //     characterEffectManager.PlayHealFX(playerManager.transform.position);

                // }
                // else
                // {
                //     currentHealth = maxHealth;
                //     healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);
                //     characterSoundFXManager.PlayHeal();
                //     characterEffectManager.PlayHealFX(playerManager.transform.position);
                // }

                healthbar.SetCurrentHealth(currentHealth);
                stamina = 0;
                manaBar.SetCurrentMana(stamina);

            }
        }

        public void TakeDamage(int damage)
        {


            if (playerManager.isInvulnerable)
                return;

            stamina = Mathf.Clamp(stamina + damage / 4, 0, maxStamina);

            // if ((stamina + damage / 4) <= maxStamina)
            // {
            //     stamina += damage / 4;
            //     manaBar.SetCurrentMana(stamina);
            // }
            // else
            // {
            //     stamina = maxStamina;
            //     manaBar.SetCurrentMana(stamina);

            // }

            characterSoundFXManager.PlayRandomDamageSoundFX(damage);
            weaponManager.CloseDamageCollider();
            weaponManager.DisableSpecialDamage();

            if (isDead)
                return;
            currentHealth -= damage;
            //change color
            healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);

            healthbar.SetCurrentHealth(currentHealth);

            if (!animator.GetBool("isInteracting"))
            {

                animator.Play("Damage");
            }
            if (damage >= 50)
            {
                animatorHandler.PlayTargetAnimation("Stunned", true);
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                deathAnimator.Play("DiedFadeIn");
                characterSoundFXManager.PlayRandomDeathSound();
                isDead = true;
            }
        }

    }
}
