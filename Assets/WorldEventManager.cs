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
        public AudioClip FirstKnightPhaseShiftBGM;

        public bool bossFightIsActive;

        public bool bossHasBeenAwakened;
        public bool bossHasBeenDefeated;

        EnemyManager enemyManager;
        bool isPlaying = false;

        private void Update()
        {
            HandlePhaseChangeBGM();
        }
        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            audioSource = GetComponent<AudioSource>();
            enemyManager = FindObjectOfType<EnemyManager>();
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
                audioSource.loop = true;
            }


            //active block
        }
        public void HandlePhaseChangeBGM()
        {
            if (!isPlaying)
            {
                if (bossHealthBar.bossName.text == "The First Knight" && enemyManager.isPhaseShifting)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(FirstKnightPhaseShiftBGM, 0.6f);
                    audioSource.loop = true;
                    isPlaying = true;
                }
            }
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            //disable block
        }
    }
}
