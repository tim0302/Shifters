using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shifters
{
    public class ManaBar : MonoBehaviour
    {
        public Slider slider;
        public void SetMaxMana(int maxMana)
        {
            slider.maxValue = maxMana;
        }
        public void SetCurrentMana(int currentMana)
        {
            slider.value = currentMana;
        }

        public void TopUpMana()
        {
            slider.value = slider.maxValue;
        }
    }
}
