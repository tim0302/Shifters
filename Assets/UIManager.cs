using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class UIManager : MonoBehaviour
    {
        public GameObject lockOnIndicator;
        CameraHandler cameraHandler;

        private void Awake()
        {
            TurnOffLockOnIndicator();
        }
        public void TurnOnLockOnIndicator()
        {
            lockOnIndicator.SetActive(true);
        }

        public void TurnOffLockOnIndicator()
        {
            lockOnIndicator.SetActive(false);
        }
    }
}

