using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WorldEventManager : MonoBehaviour
    {
        public UIBossHealthBar bossHealthBar;
        public EnemyBossManager boss;

        public bool bossFightIsActive;

        public bool bossHasBeenAwakened;
        public bool bossHasBeenDefeated;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        public void ActivateBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.SetUIHealthBarToActive();
            //active block
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            //disable block
        }
    }
}
