using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class GateManager : MonoBehaviour
    {
        WorldEventManager worldEventManager;
        public GameObject door;
        bool isDoorLocked;
        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void Update()
        {
            if (worldEventManager.bossFightIsActive && !isDoorLocked)
            {
                LockDoor();
            }
        }

        private void LockDoor()
        {
            isDoorLocked = true;
            door.transform.position -= new Vector3(0, 3, 0);
        }
    }
}
