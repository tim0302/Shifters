using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WeaponHolder : MonoBehaviour
    {
        public Transform parentOverride;
        public GameObject currentWeaponModel;
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;

            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
            currentWeaponModel = model;
        }
    }
}
