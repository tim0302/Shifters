using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        bool isPlayerSpecialAttack = false;
        bool isEnemySpecialAttack = false;
        int enemySpecialAttackDamageMultiplier = 2;
        public int currentWeaponDamage = 10;
        PlayerStats playerStats;
        PlayerManager playerManager;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
            playerStats = FindObjectOfType<PlayerStats>();
        }
        //player
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        public void EnableSpecialDamage()
        {
            isPlayerSpecialAttack = true;
        }

        public void DisableSpecialDamage()
        {
            isPlayerSpecialAttack = false;
        }
        //enemy

        public void EnableEnemySpecialDamage()
        {
            isEnemySpecialAttack = true;
        }

        public void DisableEnemySpecialDamage()
        {
            isEnemySpecialAttack = false;
        }
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                CharacterEffectManager playerCharacterEffectManager = collision.GetComponent<CharacterEffectManager>();

                PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(isEnemySpecialAttack ? currentWeaponDamage * enemySpecialAttackDamageMultiplier : currentWeaponDamage);
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    if (!playerStats.playerManager.isInvulnerable)
                    {
                        playerCharacterEffectManager.PlayOnHitFX(contactPoint);
                    }
                }
            }

            if (collision.tag == "Enemy")
            {
                CharacterEffectManager enemyCharacterEffectManager = collision.GetComponent<CharacterEffectManager>();

                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    enemyCharacterEffectManager.PlayOnHitFX(contactPoint);
                    int incomingDamage = isPlayerSpecialAttack ? currentWeaponDamage * playerStats.playerSpecialAttackDamageMultiplier : currentWeaponDamage;
                    enemyStats.TakeDamage(incomingDamage);
                    playerStats.GainStaminaByDamage(incomingDamage);
                    if (isPlayerSpecialAttack)
                    {
                        playerStats.stamina = 0;
                        playerStats.manaBar.SetCurrentMana(0);
                    }
                }
            }
        }
    }
}
