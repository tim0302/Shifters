using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class RisingStormProjectile : MonoBehaviour
    {
        public LayerMask isEnemy;
        public int explosionDamage;
        public float maxLifetime;
        public bool explodeOnTouch = false;
        public float explosionRange;
        PhysicMaterial physics_material;
        bool damageDealt = false;
        private void Start()
        {
            Setup();
        }

        private void Update()
        {
            maxLifetime -= Time.deltaTime;
            if (maxLifetime <= 0) Explode();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Projectile")) return;
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, isEnemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                PlayerStats characterStats = enemies[i].transform.GetComponent<PlayerStats>();
                characterStats.TakeDamage(explosionDamage);
            }
            Destroy(gameObject);

        }
        private void Explode()
        {
            Destroy(gameObject);
        }

        private void Delay()
        {
            Destroy(gameObject);
        }
        private void Setup()
        {
            physics_material = new PhysicMaterial();
            physics_material.frictionCombine = PhysicMaterialCombine.Minimum;
            physics_material.bounceCombine = PhysicMaterialCombine.Maximum;
        }
    }
}
