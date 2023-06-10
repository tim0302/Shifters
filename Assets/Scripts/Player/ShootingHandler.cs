using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class ShootingHandler : MonoBehaviour
    {
        public GameObject projectile;
        EnemyManager enemyManager;
        public GameObject distortion;

        public float shootForce, upwardForce;

        public float timeBetweenShooting, spread, reloadTime, positionSpread;

        bool isShooting;
        bool readyToShoot = false;
        InputHandler inputHandler;
        public Transform attackPoint;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            enemyManager = GetComponent<EnemyManager>();
        }
        void Update()
        {
            HandleShooting();
        }

        private void EnableShooting()
        {
            readyToShoot = true;
        }

        private void HandleShooting()
        {

            if (enemyManager.currentTarget != null && readyToShoot)
            {
                Shoot();
            }
        }

        private void ResetShot()
        {
            readyToShoot = true;
        }

        private void Shoot()
        {
            readyToShoot = false;

            Vector3 direction = enemyManager.currentTarget.transform.position - attackPoint.transform.position;
            //deal with spread

            Vector3 positonWithSpread = attackPoint.position + new Vector3(Random.Range(-positionSpread, positionSpread), Random.Range(-positionSpread, positionSpread), Random.Range(-positionSpread, positionSpread));
            GameObject currentprojectile = Instantiate(projectile, positonWithSpread, Quaternion.identity);
            GameObject currentDistortion = Instantiate(distortion, positonWithSpread, Quaternion.identity);
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            float z = Random.Range(-spread, spread);

            Vector3 directionWithSpread = direction + new Vector3(x, y, z);
            currentprojectile.transform.forward = directionWithSpread.normalized;
            currentDistortion.transform.forward = direction.normalized;

            currentprojectile.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            Invoke(nameof(ResetShot), timeBetweenShooting);
        }
    }
}
