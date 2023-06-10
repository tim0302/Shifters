using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WindSlashProjectile : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public GameObject explosion;
        public LayerMask isEnemy;

        [Range(0f, 1f)]
        public float bounciness;
        public bool useGravity;

        public float explosionRange;
        public int explosionDamage;

        public int maxCollisions;
        public float maxLifetime;
        public bool explodeOnTouch = true;

        int collisions;
        PhysicMaterial physics_material;

        private void Start()
        {
            Setup();
        }

        private void Update()
        {
            if (collisions > maxCollisions)
            {
                Explode();
            }
            maxLifetime -= Time.deltaTime;
            if (maxLifetime <= 0) Explode();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Projectile")) return;
            // if (collision.collider.CompareTag("Friendly")) return;

            collisions++;
        }
        private void Explode()
        {
            if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, isEnemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                PlayerStats characterStats = enemies[i].transform.GetComponent<PlayerStats>();
                characterStats.TakeDamage(explosionDamage);
            }
            Destroy(gameObject);
        }

        private void Delay()
        {
            Destroy(gameObject);
        }
        private void Setup()
        {
            physics_material = new PhysicMaterial();
            physics_material.bounciness = bounciness;
            physics_material.frictionCombine = PhysicMaterialCombine.Minimum;
            physics_material.bounceCombine = PhysicMaterialCombine.Maximum;
            rigidbody.useGravity = useGravity;
        }
    }
}
