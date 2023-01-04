using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WorldEventManager : MonoBehaviour
    {
        public UIBossHealthBar bossHealthBar;
        public EnemyBossManager boss;
        AudioSource audioSource;
        public AudioClip FirstKnightBGM;
        public bool bossFightIsActive;

        public bool bossHasBeenAwakened;
        public bool bossHasBeenDefeated;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            audioSource = GetComponent<AudioSource>();
        }

        public void ActivateBossFight()
        {
            if (bossFightIsActive)
                return;
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.SetUIHealthBarToActive();
            if (bossHealthBar.bossName.text == "The First Knight")
            {
                audioSource.PlayOneShot(FirstKnightBGM, 0.5f);
            }
            //active block
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            //disable block
        }
    }
}
